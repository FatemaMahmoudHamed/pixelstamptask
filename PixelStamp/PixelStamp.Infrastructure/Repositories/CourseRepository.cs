using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixelStamp.Infrastructure.Repositories
{
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public IUserService _userService;
        public CourseRepository(IUserService userService, CommandDbContext context, IMapper mapperConfig, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
            : base(context, mapperConfig, httpContextAccessor)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync(bool? allCourses)
        {
            var entityDbSet = commandDb.Courses;
            IList<Course> query = null;
            if (allCourses.Value)
            {
                query = entityDbSet.OrderBy(n => n.Name)
                                .AsNoTracking()
                                .AsQueryable().ToList();
            }
            if (allCourses.Value && CurrentUser.Roles.FirstOrDefault() == "Student")
            {
                query = entityDbSet.OrderBy(n => n.Name)
                                .Where(c => !c.Buyers.Any(x => x.Id == CurrentUser.UserId))
                                .AsNoTracking()
                                .AsQueryable().ToList();
            }
            if (!allCourses.Value && CurrentUser.Roles.FirstOrDefault() == "Teacher")
            {
                query = entityDbSet.OrderBy(n => n.Name)
                                .Include(l => l.Lessons)
                                .Include(b => b.Buyers)
                                .Where(c => c.CourseOwner == CurrentUser.UserId)
                                .AsNoTracking()
                                .AsQueryable().ToList();
            }
            if(!allCourses.Value && CurrentUser.Roles.FirstOrDefault() == "Student")
            {
                query = entityDbSet.Include(l => l.Lessons)
                                .OrderBy(n => n.Name)
                                .Where(c => c.Buyers.Any(x => x.Id == CurrentUser.UserId))
                                .AsNoTracking()
                                .AsQueryable().ToList();
            }
           
            return Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(query);
        }


        public async Task<CourseDto> GetAsync(int id)
        {
            var entityDbSet = commandDb.Courses;

            // Get data from database
            var course = await entityDbSet.Include(c => c.Lessons).Include(b=>b.Buyers).FirstOrDefaultAsync(m => m.Id == id);
            return mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> GetByNameAsync(string name)
        {
            var entityDbSet = commandDb.Courses;

            // Get data from database
            var course = await entityDbSet.Include(s => s.Lessons)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(m => m.Name == name);
            return mapper.Map<CourseDto>(course);
        }

        public async Task AddAsync(CourseDto CourseDto)
        {
            var entityDbSet = commandDb.Courses;
            var entityToAdd = mapper.Map<Course>(CourseDto);
            entityToAdd.Id = (await entityDbSet.IgnoreQueryFilters().MaxAsync(c => (int?)c.Id) ?? 0) + 1;
            entityToAdd.CourseOwner = CurrentUser.UserId;
            entityToAdd.CreatedOn = DateTime.Now;
            await entityDbSet.AddAsync(entityToAdd);
            await commandDb.SaveChangesAsync();

            mapper.Map(entityToAdd, CourseDto);
        }
        public async Task BuyCourseAsync(int courseID)
        {
            var studentToUpdate = await commandDb.Users.FindAsync(CurrentUser.UserId);
            var courseToUpdate = await commandDb.Courses.FindAsync(courseID);
            if (courseToUpdate.Price <= studentToUpdate.Budget) 
            {
                studentToUpdate.Budget = studentToUpdate.Budget - courseToUpdate.Price;
            }
            studentToUpdate.Courses.Add(courseToUpdate);
            studentToUpdate.UpdatedOn = DateTime.Now;
            await commandDb.SaveChangesAsync();
            //teacher
            var TeacherToUpdate = await commandDb.Users.FindAsync(courseToUpdate.CourseOwner);
            TeacherToUpdate.Budget = TeacherToUpdate.Budget + courseToUpdate.Price;
            TeacherToUpdate.UpdatedOn = DateTime.Now;
            await commandDb.SaveChangesAsync();
            //course
            courseToUpdate.Buyers.Add(studentToUpdate);
            await commandDb.SaveChangesAsync();

        }

        public async Task EditAsync(CourseDto CourseDto)
        {
            var entityDbSet = commandDb.Courses;

            var entityToUpdate = await entityDbSet.FindAsync(CourseDto.Id);
            mapper.Map(CourseDto, entityToUpdate);
            await commandDb.SaveChangesAsync();

            mapper.Map(entityToUpdate, CourseDto);
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = commandDb.Courses;

            var entityToDelete = await entityDbSet.FindAsync(id);
            entityDbSet.Remove(entityToDelete);
            await commandDb.SaveChangesAsync();
        }

    }
}
