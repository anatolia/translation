using System.Threading.Tasks;
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
            var model = new LanguageCreateModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageCreateModel model)
        {
            if (!model.Validate())
            {
                return View(model);
            }

            //todo: map request and post

            var uid = "";
            return RedirectToAction("List", "Language", uid);

            return View();
        }

        [HttpGet]
        public JsonResult Items()
        {
            return Json(null);
        }
    }
}
