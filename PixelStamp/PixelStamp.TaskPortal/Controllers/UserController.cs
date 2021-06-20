using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISolutionTask.Portal.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PixelStamp.Core.Interfaces.Services;

namespace ISolutionTask.Controllers
{
    public class UserController : BaseController
    {
    
        private readonly IUserService _userService;

        public UserController(IUserService userService,IHttpContextAccessor httpContextAccessor)
            :base(httpContextAccessor)
        {
           
            _userService = userService;
        }

        public IActionResult Index()
        {

            //bool IsAdmin = _userService.IsAdminUser(LoggedInUserId);
            ViewBag.IsAdmin = IsAdmin;
            return View();
        }
    }
}