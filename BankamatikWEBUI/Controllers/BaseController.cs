using Microsoft.AspNetCore.Mvc;

namespace BankamatikWEBUI.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("UserID") != null;
        }

        protected IActionResult RedirectToLoginIfUnauthenticated()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }

            return null;
        }
    }
}
