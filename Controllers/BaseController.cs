using Microsoft.AspNetCore.Mvc;

namespace MapTask.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account");
        }

        protected IActionResult RedirectToMap()
        {
            return RedirectToAction("Index", "Map");
        }

    }
}
