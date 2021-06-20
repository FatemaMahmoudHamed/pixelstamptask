using PixelStamp.Core.Dtos;
using PixelStamp.Core.Models;
using System.Threading.Tasks;

namespace PixelStamp.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ReturnResult<string>> Login(CredentialsDto credentials, string rule_name = null);
        Task<ReturnResult<string>> Register(UserDto model);
        //register

    }
}