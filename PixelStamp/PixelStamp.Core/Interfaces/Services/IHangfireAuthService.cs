using PixelStamp.Core.Dtos;
using PixelStamp.Core.Models;
using System.Threading.Tasks;

namespace PixelStamp.Interfaces.Services
{
    public interface IHangfireAuthService
    {
        Task<ReturnResult<string>> Login(CredentialsDto credentials);
        Task<ReturnResult<string>> Register(UserDto model);

    }
}