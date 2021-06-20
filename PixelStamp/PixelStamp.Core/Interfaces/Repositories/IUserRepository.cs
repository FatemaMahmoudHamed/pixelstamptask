using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<UserDto> GetAsync(Guid id);
        Task EditAsync(UserDto userDto);
        Task AddAsync(UserDto userDto);

        Task<AppUser> GetByUserName(string userName);

        Task<bool> CheckUserExists(string userName);

    }
}