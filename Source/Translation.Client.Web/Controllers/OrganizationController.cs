using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Models;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Controllers
{
    public class OrganizationController : BaseController
    {
        [HttpGet]
        public IActionResult Detail(string id)
        {
            if (id.IsNotUid())
            {
                return RedirectToAccessDenied();
            }

            return View();
        }
    }
}
