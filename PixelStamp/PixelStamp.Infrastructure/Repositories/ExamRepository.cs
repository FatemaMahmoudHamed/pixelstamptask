using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixelStamp.Infrastructure.Repositories
{
    public class ExamRepository : RepositoryBase, IExamRepository
    {
        public ExamRepository(CommandDbContext context, IMapper mapperConfig, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
            : base(context, mapperConfig, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<Exam>> GetAllAsync(int courseId)
        {
            var entityDbSet = commandDb.Exams.Include(c => c.Course).Include(q=>q.Questions);

            // Set query data from database
            return entityDbSet.Where(res=>res.CourseId==courseId).AsQueryable().ToList();
            
        }

        public async Task<Exam> GetAsync(int id)
        {
            var entityDbSet = commandDb.Exams.Include(d => d.Course).Include(q=>q.Questions);

            // Get data from database
            var lesson = await entityDbSet.FirstOrDefaultAsync(m => m.Id == id);

            return mapper.Map<Exam>(lesson);
        }

        public async Task<Exam> AddAsync(Exam Exam)
        {
            try {
                var entityDbSet = commandDb.Exams;
                var entityToAdd = new Exam();
                entityToAdd.CourseId = Exam.CourseId;
                entityToAdd.Questions = Exam.Questions;
                entityToAdd.CreatedOn = DateTime.Now;
                await entityDbSet.AddAsync(entityToAdd);
                await commandDb.SaveChangesAsync();
                return Exam;
            }
            catch (Exception ex) { var e = ex.Message; return null; }
            
        }

        public async Task EditAsync(Exam Exam)
        {
            var entityDbSet = commandDb.Exams;
            var entityToUpdate = await entityDbSet.FindAsync(Exam.Id);
            entityToUpdate.Questions = Exam.Questions;
            await commandDb.SaveChangesAsync();

            mapper.Map(entityToUpdate, Exam);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entityDbSet = commandDb.Exams;

                var entityToDelete = await entityDbSet.FindAsync(id);
                entityDbSet.Remove(entityToDelete);
                await commandDb.SaveChangesAsync();
            }
            catch (Exception ex) {
                var e = ex.Message;
            }
        }
        
    }
}
