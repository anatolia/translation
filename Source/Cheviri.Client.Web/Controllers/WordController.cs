using Microsoft.AspNetCore.Mvc;

using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models;

namespace Cheviri.Client.Web.Controllers
{
    public class WordController : BaseController
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
        public JsonResult Items(string term, int skip = 0, int take = 20)
        {
            return View();
        }
    }
}
