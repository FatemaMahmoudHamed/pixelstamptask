using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using PixelStamp.Core.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace PixelStamp.Portal.Filters
{
    public class HangFireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (httpContext.Session.GetString("token") != null)
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(httpContext.Session.GetString("token"));

                if (token.Claims.Any(n => n.Type == "roles" && n.Value == ApplicationRolesConstants.Teacher.Name))
                    return true;
            }
            return false;
        }
    }
}
