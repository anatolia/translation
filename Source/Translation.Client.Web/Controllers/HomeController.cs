using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Models;

namespace Translation.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeModel();
            model.IsSuperAdmin = CurrentUser?.IsSuperAdmin ?? false;
            model.IsAuthenticated = CurrentUser != null;
            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied()
        {
            var model = new AccessDeniedModel();
            return View(model);
        }
    }
}
