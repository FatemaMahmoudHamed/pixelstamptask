using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelStamp.Portal.Models;
using System.Diagnostics;

namespace PixelStamp.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger):base()
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PrepareViewBags();
            return View();
        }

        public IActionResult Privacy()
        {
            PrepareViewBags();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            PrepareViewBags();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void PrepareViewBags()
        {
            ViewBag.LoggedInUserId = LoggedInUserId;
            ViewBag.LoggedInUserName = LoggedInUserName;
            ViewBag.Role = Role;
        }
    }

}
