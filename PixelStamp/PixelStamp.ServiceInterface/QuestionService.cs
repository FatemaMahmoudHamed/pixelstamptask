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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _QuestionRepository;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(IQuestionRepository QuestionRepository, ILogger<QuestionService> logger)
        {
            _QuestionRepository = QuestionRepository;
            _logger = logger;
        }

        public async Task<ReturnResult<IEnumerable<Question>>> GetAllAsync()
        {
            try
            {
                var entities = await _QuestionRepository.GetAllAsync();

                return new ReturnResult<IEnumerable<Question>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<IEnumerable<Question>>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }

                };
            }
        }

        public async Task<ReturnResult<Question>> GetAsync(int id)
        {
            try
            {
                var entitiy = await _QuestionRepository.GetAsync(id);

                return new ReturnResult<Question>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Question>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<Question>> AddAsync(Question model)
        {
            try
            {
                await _QuestionRepository.AddAsync(model);

                return new ReturnResult<Question>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Question>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<Question>> EditAsync(Question model)
        {
            try
            {
                await _QuestionRepository.EditAsync(model);

                return new ReturnResult<Question>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return new ReturnResult<Question>
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
                await _QuestionRepository.RemoveAsync(id);

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
