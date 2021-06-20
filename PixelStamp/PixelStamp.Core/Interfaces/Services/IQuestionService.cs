using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<ReturnResult<IEnumerable<Question>>> GetAllAsync();

        Task<ReturnResult<Question>> GetAsync(int id);

        Task<ReturnResult<Question>> AddAsync(Question model);

        Task<ReturnResult<Question>> EditAsync(Question model);

        Task<ReturnResult<bool>> RemoveAsync(int id);


    }
}
