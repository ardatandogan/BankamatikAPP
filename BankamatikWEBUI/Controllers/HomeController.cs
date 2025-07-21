using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Bankamatik.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Oturumda kullan�c� var m� kontrol et
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                // E�er yoksa login sayfas�na y�nlendir
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Username = username;
            return View();
        }
    }
}
