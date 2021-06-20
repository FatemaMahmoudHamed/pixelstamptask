using Microsoft.Extensions.Logging;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Enums;
using PixelStamp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.ServiceInterface
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _ExamRepository;
        private readonly ILogger<ExamService> _logger;

        public ExamService(IExamRepository ExamRepository, ILogger<ExamService> logger)
        {
            _ExamRepository = ExamRepository;
            _logger = logger;
        }

        public async Task<ReturnResult<IEnumerable<Exam>>> GetAllAsync(int CourseId)
        {
            try
            {
                var entities = await _ExamRepository.GetAllAsync(CourseId);

                return new ReturnResult<IEnumerable<Exam>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<IEnumerable<Exam>>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<Exam>> GetAsync(int id)
        {
            try
            {
                var entitiy = await _ExamRepository.GetAsync(id);

                return new ReturnResult<Exam>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Exam>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<Exam>> AddAsync(Exam model)
        {
            try
            {
                await _ExamRepository.AddAsync(model);

                return new ReturnResult<Exam>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Exam>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<Exam>> EditAsync(Exam model)
        {
            try
            {
                await _ExamRepository.EditAsync(model);

                return new ReturnResult<Exam>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Exam>
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
                await _ExamRepository.RemoveAsync(id);

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
