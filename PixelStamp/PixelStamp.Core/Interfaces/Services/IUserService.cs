using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ReturnResult<UserDto>> GetAsync(Guid id);

        Task<ReturnResult<AppUser>> GetByUserName(string userName);

        Task<ReturnResult<UserDto>> AddAsync(UserDto model);

        Task<ReturnResult<UserDto>> EditAsync(UserDto model);
    }

}
