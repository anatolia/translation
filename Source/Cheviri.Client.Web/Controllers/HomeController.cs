using Microsoft.AspNetCore.Mvc;

namespace Cheviri.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
