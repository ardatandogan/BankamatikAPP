using Microsoft.AspNetCore.Mvc;
using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Bankamatik.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AccountService _accountService;

        public AuthController()
        {
            _userService = new UserService(new Bankamatik.DataAccess.Repositories.UserRepository());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                // Giriş yapılmamış, login sayfasına yönlendir
                return RedirectToAction("Login", "Auth");
            }

            var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
            return View(accounts);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.GetUserByUsername(new User { Username = username });

            if (user != null && user.PasswordHash == password)  // Gerçek projede hash karşılaştırmalı
            {
                // Session'a kullanıcı bilgilerini ekle
                HttpContext.Session.SetInt32("userId", user.ID);
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetString("role", user.Role);

                // Giriş başarılı, kullanıcı sayfasına yönlendir
                return RedirectToAction("Index", "User");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
