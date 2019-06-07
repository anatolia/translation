using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Models;

namespace Translation.Client.Web.Controllers
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
            return Json(null);
        }
    }
}
