using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Cheviri.Client.Web.Models;

namespace Cheviri.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied()
        {
            var model = new AccessDeniedModel();
            return View(model);
        }
    }
}
