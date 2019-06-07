using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Models;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Controllers
{
    public class LabelController : BaseController
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            if (id.IsNotUid())
            {
                return RedirectToAccessDenied();
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateTranslation()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Items()
        {
            return Json(null);
        }
    }
}
