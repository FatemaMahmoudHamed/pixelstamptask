using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISolutionTask.Portal.Controllers
{
    public class BaseController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string LoggedInUserId { get { return (User.FindFirstValue(ClaimTypes.NameIdentifier)); } }//  .Claims.Where(c => c.Type == "userid").SingleOrDefault().Value); } }

        public string LoggedInUserName { get { return (_httpContextAccessor.HttpContext.User.Identity.Name).ToString(); } }//  .Claims.Where(c => c.Type == "userid").SingleOrDefault().Value); } }
   


    }
}