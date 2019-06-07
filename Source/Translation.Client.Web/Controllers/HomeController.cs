using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Models;

namespace Translation.Client.Web.Controllers
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
