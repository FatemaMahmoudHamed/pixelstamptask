using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<ReturnResult<IEnumerable<CourseDto>>> GetAllAsync(bool? allCourses);

        Task<ReturnResult<CourseDto>> GetAsync(int id);

        Task<ReturnResult<CourseDto>> AddAsync(CourseDto model);

        Task<ReturnResult<CourseDto>> EditAsync(CourseDto model);

        Task<ReturnResult<bool>> RemoveAsync(int id);

        Task<ReturnResult<bool>> BuyCourseAsync(int courseId,Guid userId );

    }
}
