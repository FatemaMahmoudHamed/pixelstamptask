using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixelStamp.Infrastructure.Repositories
{
    public class LessonRepository : RepositoryBase, ILessonRepository
    {
        public LessonRepository(CommandDbContext context, IMapper mapperConfig, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
            : base(context, mapperConfig, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<LessonDto>> GetAllAsync(int courseId)
        {
            var entityDbSet = commandDb.Lessons.Include(c => c.Course);

            // Set query data from database
            var query = entityDbSet.OrderBy(n => n.Description).Where(res=>res.CourseId==courseId).AsQueryable().ToList();

            return Mapper.Map<IEnumerable<Lesson>, IEnumerable<LessonDto>>(query);
        }

        public async Task<LessonDto> GetAsync(int id)
        {
            var entityDbSet = commandDb.Lessons.Include(d => d.Course);

            // Get data from database
            var lesson = await entityDbSet.FirstOrDefaultAsync(m => m.Id == id);

            return mapper.Map<LessonDto>(lesson);
        }

        public async Task<LessonDto> AddAsync(LessonDto LessonDto)
        {
            var entityDbSet = commandDb.Lessons;

            var entityToAdd = mapper.Map<Lesson>(LessonDto);
            entityToAdd.Id = (await entityDbSet.IgnoreQueryFilters().MaxAsync(c => (int?)c.Id) ?? 0) + 1;
            entityToAdd.CreatedOn = DateTime.Now;
            await entityDbSet.AddAsync(entityToAdd);
            await commandDb.SaveChangesAsync();

            return mapper.Map(entityToAdd, LessonDto);
        }

        public async Task EditAsync(LessonDto LessonDto)
        {
            var entityDbSet = commandDb.Lessons;
            var entityToUpdate = await entityDbSet.FindAsync(LessonDto.Id);
            entityToUpdate.Description = LessonDto.Description;
            await commandDb.SaveChangesAsync();

            mapper.Map(entityToUpdate, LessonDto);
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = commandDb.Lessons;

            var entityToDelete = await entityDbSet.FindAsync(id);
            entityDbSet.Remove(entityToDelete);
            await commandDb.SaveChangesAsync();
        }
        
    }
}
