using PixelStamp.Core.Dtos;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Repositories

{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllAsync(int CourseId);

        Task<Exam> GetAsync(int id);

        Task<Exam> AddAsync(Exam Exam);

        Task EditAsync(Exam saveExam);

        Task RemoveAsync(int id);
    }
}