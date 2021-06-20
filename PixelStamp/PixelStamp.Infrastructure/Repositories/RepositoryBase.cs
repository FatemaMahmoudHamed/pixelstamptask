using AutoMapper;
using Microsoft.AspNetCore.Http;
using PixelStamp.Core.Constants;
using System;
using System.Linq;
using System.Security.Claims;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PixelStamp.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        protected IMapper mapper;
        protected CommandDbContext commandDb;

        public RepositoryBase(CommandDbContext context, IMapper mapperConfig, IHttpContextAccessor httpContextAccessor)
        {
            commandDb = context;
            mapper = mapperConfig;

            var token = httpContextAccessor.HttpContext.Request.Headers.Where(h => h.Key == "Authorization").Select(h => h.Value).FirstOrDefault();
            var tokenObject = new JwtSecurityToken();
            if (!String.IsNullOrEmpty(token))
                tokenObject = new JwtSecurityTokenHandler().ReadJwtToken(token);


            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("id") != null
                ? Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst("id").Value)
                : new Guid();

            CurrentUser.UserId = tokenObject.Claims.Any(x=>x.Type=="id")? new Guid(tokenObject?.Claims.Where(c => c.Type == "id").Select(c => c.Value).FirstOrDefault()) : UserId;
            CurrentUser.IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            CurrentUser.UserName = tokenObject.Claims.Any(x => x.Type == "given_name") ? tokenObject?.Claims.Where(c => c.Type == "given_name").Select(c => c.Value).FirstOrDefault() : "";
            if (tokenObject.Claims.Any(x => x.Type == "roles"))
                CurrentUser.Roles.Add(tokenObject?.Claims.Where(c => c.Type == "roles").Select(c => c.Value).FirstOrDefault());

            var claimsIdentity = (ClaimsIdentity)httpContextAccessor.HttpContext?.User?.Identity;
            if (claimsIdentity != null)
            {
                // get user name
                var userName = claimsIdentity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userName != null)
                {
                    CurrentUser.UserName = userName.Value;
                }
                // get user roles
                var roles = claimsIdentity.Claims.ToList().Where(r => r.Type == ClaimsIdentity.DefaultRoleClaimType);
                foreach (var item in roles)
                {
                    CurrentUser.Roles.Add(item.Value);
                }
            }
        }

        public Guid UserId { get; private set; }

        public CurrentUser CurrentUser { get; set; } = new CurrentUser();
    }
}
