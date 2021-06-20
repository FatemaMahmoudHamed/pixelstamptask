using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Enums;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.Core.Models;
using PixelStamp.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PixelStamp.ServiceInterface
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IJwtFactory _jwtFactory;


        public AuthService(ILogger<AuthService> logger,
            UserManager<AppUser> userManager,
            IUserService userService,
            IMapper mapper,
            IJwtFactory jwtFactory)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _jwtFactory = jwtFactory;
        }

        public async Task<ReturnResult<string>> Login(CredentialsDto credentials, string rule_name = null)
        {

            try
            {
                var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
                if (identity == null)
                {
                    return new ReturnResult<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status401Unauthorized,
                        ErrorList = new List<string> { "Username or password are not correct" }
                    };

                }

                  var userToVerify = _userService.GetByUserName(credentials.UserName).Result.Data;


                if (userToVerify.IsDeleted)
                    return new ReturnResult<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatuses.Status401Unauthorized,
                        ErrorList = new List<string> { "This user has been deleted" }
                    };

                var roles = await _userManager.GetRolesAsync(userToVerify);
                // jwt object
                var response = await _jwtFactory.GenerateEncodedToken(
                        credentials.UserName,
                        identity,
                        roles);

                return new ReturnResult<string>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ReturnResult<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }
                };
            }
        }

        public async Task<ReturnResult<string>> Register(UserDto model)
        {
            try
            {
                await _userService.AddAsync(model);
                var response = await Login(new CredentialsDto { UserName = model.UserName, Password = model.Password });

                return new ReturnResult<string>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatuses.Status200OK,
                    Data = response.Data
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ReturnResult<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatuses.Status500InternalServerError,
                    ErrorList = new List<string> { "Fetal error has been happened" }

                };
            }
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName.ToString()) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName.ToString());

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var roles = await _userManager.GetRolesAsync(userToVerify);
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName.ToString(), userToVerify.Id.ToString(),roles.FirstOrDefault()));
            }
            return await Task.FromResult<ClaimsIdentity>(null);
        }
       
    }
}
