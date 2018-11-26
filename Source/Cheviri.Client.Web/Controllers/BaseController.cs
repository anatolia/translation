using Microsoft.AspNetCore.Mvc;

namespace Cheviri.Client.Web.Controllers
{
    public class BaseController : Controller
    {
        public RedirectResult RedirectToHome()
        {
            return Redirect("/");
        }

        public RedirectResult RedirectToAccessDenied()
        {
            return Redirect("/Home/AccessDenied");
        }
    }
}