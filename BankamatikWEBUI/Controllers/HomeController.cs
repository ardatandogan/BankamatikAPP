using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Bankamatik.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Oturumda kullanýcý var mý kontrol et
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                // Eðer yoksa login sayfasýna yönlendir
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Username = username;
            return View();
        }
    }
}
