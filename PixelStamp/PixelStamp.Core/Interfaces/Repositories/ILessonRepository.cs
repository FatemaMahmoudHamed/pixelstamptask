using PixelStamp.Core.Dtos;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task<IEnumerable<LessonDto>> GetAllAsync(int CourseId);

        Task<LessonDto> GetAsync(int id);

        Task<LessonDto> AddAsync(LessonDto LessonDto);

        Task EditAsync(LessonDto saveLessonDto);

        Task RemoveAsync(int id);
    }
}