using Microsoft.AspNetCore.Mvc;

using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models;

namespace Cheviri.Client.Web.Controllers
{
    public class LanguageController : BaseController
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Items()
        {
            return View();
        }
    }
}
