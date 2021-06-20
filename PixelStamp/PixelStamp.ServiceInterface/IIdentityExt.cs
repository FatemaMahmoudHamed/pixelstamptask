using PixelStamp.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace ISalesWebSite.Services.Extensions
{
    public static class IIdentityExt
    {
        public static string GetFullName(this IIdentity identity)
        {
            var Claims = ((ClaimsIdentity)identity).Claims;
            var fullNameClaim = Claims.FirstOrDefault(x => x.Type == "UserName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
        public static long GetIUserID(this IIdentity identity)
        {
            var Claims = ((ClaimsIdentity)identity).Claims;
            var iUserIDClaim = Claims.FirstOrDefault(x => x.Type == "UserId");
            if (iUserIDClaim != null)
                return long.Parse(iUserIDClaim.Value);

            return 0;
        }

        public static long GetIUserCompanyID(this IIdentity identity)
        {
            var Claims = ((ClaimsIdentity)identity).Claims;
            var iUserIDClaim = Claims.FirstOrDefault(x => x.Type == "CompanyID");
            if (iUserIDClaim != null)
                return long.Parse(iUserIDClaim.Value);

            return 0;
        }


        public static string GetIUserEmail(this IIdentity identity)
        {
            var Claims = ((ClaimsIdentity)identity).Claims;
            var iUserIDClaim = Claims.FirstOrDefault(x => x.Type == "Email");
            if (iUserIDClaim != null)
                return iUserIDClaim.Value;

            return "";
        }
        
        public static List<string> GetUserRoles(this IIdentity identity)
        {
            List<string> authorizedRoles = new List<string>();

            List<string> roles = ((ClaimsIdentity)identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            roles.ForEach(role => authorizedRoles.AddRange(roles));
            return authorizedRoles;
        }

        public static bool IsAuthenticatedInRoles(this IIdentity identity, params string[] Roles)
        {
            if (Roles.Length > 0)
            {

                List<string> roles = ((ClaimsIdentity)identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();
                
                return roles.Intersect(Roles)
                    .Count() > 0 && identity.IsAuthenticated;
            }
            return false;
        }
    }
}
