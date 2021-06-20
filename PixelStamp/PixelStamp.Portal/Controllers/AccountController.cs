using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelStamp.Core.Constants;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace PixelStamp.Portal.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;

        
        public AccountController(IAuthService authService) :base()
        {
            _authService = authService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Login()
        {
            
            if (HttpContext.Session.GetString("token") != null)
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(HttpContext.Session.GetString("token"));

                if (token != null )
                {
                    return Redirect("/Home");
                }
                else if (token != null)
                {
                    HttpContext.Session.Remove("token");

                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CredentialsDto model)
        {

            var result = await _authService.Login(model);

            if (result.IsSuccess && result.ErrorList == null)
            {
                HttpContext.Session.SetString("token", result.Data);

                return RedirectToAction("Index", "Course", new { allCourses = true });
            }
            if (result.ErrorList != null)
            {
                TempData["StatusMessage"] = result.ErrorList.FirstOrDefault();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto model)
        {

            var result = await _authService.Register(model);

            if (result.IsSuccess && result.ErrorList == null)
            {

                HttpContext.Session.SetString("token", result.Data);

                return RedirectToAction("Index", "Course", new { allCourses = true });
            }
            if (result.ErrorList != null)
            {
                TempData["StatusMessage"] = result.ErrorList.FirstOrDefault();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Request.Headers.Remove("Authorization");
            HttpContext.Session.Remove("token");
            return RedirectToAction("Login");
        }

        
    }
}
