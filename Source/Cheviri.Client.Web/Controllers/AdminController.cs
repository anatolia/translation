using Microsoft.AspNetCore.Mvc;

using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models;

namespace Cheviri.Client.Web.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Journals()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PermissionLogs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserLoginLogs()
        {
            return View();  
        }

        [HttpGet]
        public IActionResult TokenUsageLogs()
        {
            return View();
        }
    }
}
