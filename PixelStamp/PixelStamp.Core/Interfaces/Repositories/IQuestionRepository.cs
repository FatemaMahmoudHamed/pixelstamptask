using PixelStamp.Core.Dtos;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Repositories

{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();

        Task<Question> GetAsync(int id);

        Task<Question> AddAsync(Question LessonDto);

        Task EditAsync(Question saveLessonDto);

        Task RemoveAsync(int id);
    }
}