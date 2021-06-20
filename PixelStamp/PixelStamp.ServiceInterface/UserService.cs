using Microsoft.Extensions.Logging;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            ILogger<UserService> logger
            )
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public async Task<ReturnResult<UserDto>> GetAsync(Guid id)
        {
            try
            {
                var entitiy = await _userRepository.GetAsync(id);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<UserDto>> AddAsync(UserDto model)
        {
            try
            {
                var errors = new List<string>();
                var validationResult = ValidationResult.CheckModelValidation(new UserValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }

                await _userRepository.AddAsync(model);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status201Created,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, model);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<AppUser>> GetByUserName(string userName)
        {
            try
            {
                var entitiy = await _userRepository.GetByUserName(userName);

                return new ReturnResult<AppUser>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = entitiy
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, userName);

                return new ReturnResult<AppUser>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<UserDto>> EditAsync(UserDto model)
        {
            try
            {
                var errors = new List<string>();
                var validationResult = ValidationResult.CheckModelValidation(new UserValidator(), model);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors.ToList());
                }

                await _userRepository.EditAsync(model);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, model);

                return new ReturnResult<UserDto>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }


    }
}
