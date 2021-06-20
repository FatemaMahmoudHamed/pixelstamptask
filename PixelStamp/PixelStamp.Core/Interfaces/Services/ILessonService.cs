using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface ILessonService
    {
        Task<ReturnResult<IEnumerable<LessonDto>>> GetAllAsync(int CourseID);

        Task<ReturnResult<LessonDto>> GetAsync(int id);

        Task<ReturnResult<LessonDto>> AddAsync(LessonDto model);

        Task<ReturnResult<LessonDto>> EditAsync(LessonDto model);

        Task<ReturnResult<bool>> RemoveAsync(int id);

        //Task<ReturnResult<bool>> IsNameExistsAsync(LessonDto LessonDto);

    }
}
