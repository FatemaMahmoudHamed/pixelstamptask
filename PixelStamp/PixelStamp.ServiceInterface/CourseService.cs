using Microsoft.Extensions.Logging;
using PixelStamp.Core.Constants;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Enums;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Core.Models;
using PixelStamp.ServiceInterface.Validators.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelStamp.ServiceInterface
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly IUserRepository _userRepository;


        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository CourseRepository, IUserRepository userRepository, ILogger<CourseService> logger)
        {
            _CourseRepository = CourseRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ReturnResult<IEnumerable<CourseDto>>> GetAllAsync(bool? allCourses)
        {
            try
            {
                var entities = await _CourseRepository.GetAllAsync(allCourses);

                return new ReturnResult<IEnumerable<CourseDto>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<IEnumerable<CourseDto>>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<CourseDto>> GetAsync(int id)
        {
            try
            {
                var entitiy = await _CourseRepository.GetAsync(id);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<CourseDto>> GetByNameAsync(string name)
        {
            try
            {
                var entitiy = await _CourseRepository.GetByNameAsync(name);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<CourseDto>> AddAsync(CourseDto model)
        {
            try
            {
                var errors = new List<string>();
                var validationResult = ValidationResult.CheckModelValidation(new CourseValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }

                if (errors.Any())
                {
                    return new ReturnResult<CourseDto>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status400BadRequest,
                        ErrorList = errors,
                        Data = null
                    };
                }

                await _CourseRepository.AddAsync(model);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }

                };
            }
        }

        public async Task<ReturnResult<bool>> BuyCourseAsync(int courseId, Guid userId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var course = await _CourseRepository.GetAsync(courseId);
                if (user == null || course == null) 
                {
                    return new ReturnResult<bool>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status404NotFound,
                        Data = false
                    };
                }
                if (user.Roles.FirstOrDefault() != ApplicationRolesConstants.Student.Name || course.Price > user.Budget)
                {
                    return new ReturnResult<bool>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status401Unauthorized,
                        Data = false
                    };
                }

                await _CourseRepository.BuyCourseAsync(courseId);

                return new ReturnResult<bool>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<bool>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }


        public async Task<ReturnResult<CourseDto>> EditAsync(CourseDto model)
        {
            try
            {
                var errors = new List<string>();

                var validationResult = ValidationResult.CheckModelValidation(new CourseValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }

                if (errors.Any())
                {
                    return new ReturnResult<CourseDto>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status400BadRequest,
                        ErrorList = errors,
                        Data = null
                    };
                }

                await _CourseRepository.EditAsync(model);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<CourseDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<bool>> RemoveAsync(int id)
        {
            try
            {

                await _CourseRepository.RemoveAsync(id);

                return new ReturnResult<bool>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);

                return new ReturnResult<bool>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

    }
}
