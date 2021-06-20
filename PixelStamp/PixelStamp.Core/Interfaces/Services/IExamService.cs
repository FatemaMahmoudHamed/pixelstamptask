using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface IExamService
    {
        Task<ReturnResult<IEnumerable<Exam>>> GetAllAsync(int CourseID);

        Task<ReturnResult<Exam>> GetAsync(int id);

        Task<ReturnResult<Exam>> AddAsync(Exam model);

        Task<ReturnResult<Exam>> EditAsync(Exam model);

        Task<ReturnResult<bool>> RemoveAsync(int id);

    }
}
