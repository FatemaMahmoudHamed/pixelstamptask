using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Repositories
{
    public interface ICourseRepository
    {

        Task<IEnumerable<CourseDto>> GetAllAsync(bool? allCourses);

        Task<CourseDto> GetAsync(int id);

        Task<CourseDto> GetByNameAsync(string name);

        Task AddAsync(CourseDto CourseDto);

        Task BuyCourseAsync(int courseId);

        Task EditAsync(CourseDto saveCourseDto);

        Task RemoveAsync(int id);
    }
}