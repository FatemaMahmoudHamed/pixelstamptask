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
    public class QuestionRepository : RepositoryBase, IQuestionRepository
    {
        public QuestionRepository(CommandDbContext context, IMapper mapperConfig, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
            : base(context, mapperConfig, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            var entityDbSet = commandDb.Questions;
            // Set query data from database
            return entityDbSet.AsQueryable().ToList();
            
        }

        public async Task<Question> GetAsync(int id)
        {
            var entityDbSet = commandDb.Questions;

            // Get data from database
            var lesson = await entityDbSet.FirstOrDefaultAsync(m => m.Id == id);

            return mapper.Map<Question>(lesson);
        }

        public async Task<Question> AddAsync(Question Question)
        {
            var entityDbSet = commandDb.Questions;
            var entityToAdd = new Question();
            entityToAdd.Id = (await entityDbSet.IgnoreQueryFilters().MaxAsync(c => (int?)c.Id) ?? 0) + 1;
            await entityDbSet.AddAsync(entityToAdd);
            await commandDb.SaveChangesAsync();

            return mapper.Map(entityToAdd, Question);
        }

        public async Task EditAsync(Question Question)
        {
            var entityDbSet = commandDb.Questions;
            var entityToUpdate = await entityDbSet.FindAsync(Question.Id);
            await commandDb.SaveChangesAsync();

            mapper.Map(entityToUpdate, Question);
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = commandDb.Questions;

            var entityToDelete = await entityDbSet.FindAsync(id);
            entityDbSet.Remove(entityToDelete);
            await commandDb.SaveChangesAsync();
        }
        
    }
}
