using Microsoft.Extensions.Logging;
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
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _LessonRepository;
        private readonly ILogger<LessonService> _logger;

        public LessonService(ILessonRepository LessonRepository, ILogger<LessonService> logger)
        {
            _LessonRepository = LessonRepository;
            _logger = logger;
        }

        public async Task<ReturnResult<IEnumerable<LessonDto>>> GetAllAsync(int CourseId)
        {
            try
            {
                var entities = await _LessonRepository.GetAllAsync(CourseId);

                return new ReturnResult<IEnumerable<LessonDto>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<IEnumerable<LessonDto>>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<LessonDto>> GetAsync(int id)
        {
            try
            {
                var entitiy = await _LessonRepository.GetAsync(id);

                return new ReturnResult<LessonDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<LessonDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "حدث خطأ أثناء تنفيذ العملية، يرجى المحاولة لاحقاً." }
                };
            }
        }

        public async Task<ReturnResult<LessonDto>> AddAsync(LessonDto model)
        {
            try
            {
                var errors = new List<string>();
                var validationResult = ValidationResult.CheckModelValidation(new LessonValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }

                if (errors.Any())
                {
                    return new ReturnResult<LessonDto>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status400BadRequest,
                        ErrorList = errors
                    };
                }

                await _LessonRepository.AddAsync(model);

                return new ReturnResult<LessonDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<LessonDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "حدث خطأ أثناء تنفيذ العملية، يرجى المحاولة لاحقاً." }
                };
            }
        }

        public async Task<ReturnResult<LessonDto>> EditAsync(LessonDto model)
        {
            try
            {
                var errors = new List<string>();
                var validationResult = ValidationResult.CheckModelValidation(new LessonValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }
                
                if (errors.Any())
                {
                    return new ReturnResult<LessonDto>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status400BadRequest,
                        ErrorList = errors
                    };
                }

                await _LessonRepository.EditAsync(model);

                return new ReturnResult<LessonDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<LessonDto>
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

                await _LessonRepository.RemoveAsync(id);

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
