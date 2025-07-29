using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Bankamatik.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Username = username;
            return View();
        }
    }
}
