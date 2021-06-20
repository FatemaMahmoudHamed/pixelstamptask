using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ISalesWebSite.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PixelStamp.Portal.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public JwtSecurityToken DecodedToken
        {
            get {
                var token = HttpContext.Request.Headers.Where(h => h.Key == "Authorization").Select(h => h.Value).FirstOrDefault();
                var tokenObject = new JwtSecurityToken();
                if (!String.IsNullOrEmpty(token))
                    return tokenObject = new JwtSecurityTokenHandler().ReadJwtToken(token);
                else return null;
            }
        }
        public string LoggedInUserId { get { return DecodedToken?.Claims.Where(c => c.Type == "id").Select(c => c.Value).FirstOrDefault(); } }
        public string Role { get { return DecodedToken?.Claims.Where(c => c.Type == "roles").Select(c => c.Value).FirstOrDefault(); } } 
        public string LoggedInUserName { get { return DecodedToken?.Claims.Where(c => c.Type == "sub").Select(c => c.Value).FirstOrDefault(); } }
    }
}