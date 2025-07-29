using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bankamatik.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AccountService _accountService;
        private readonly LogService _logService;

        public AuthController()
        {
            var userRepository = new UserRepository();
            var logRepository = new LogRepository();
            var logService = new LogService(logRepository);

            _userService = new UserService(userRepository, logService);
            _logService = new LogService(new LogRepository());
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
                return RedirectToAction("Login", "Auth");
            }

            var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
            return View(accounts);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.GetUserByUsername(new User { Username = username });

            if (user != null && user.PasswordHash == password) 
            {
                HttpContext.Session.SetInt32("userId", user.ID);
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetString("role", user.Role);


                return RedirectToAction("Index", "User");
            }


            ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
            return View();
        }

        public IActionResult Logout()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var username = HttpContext.Session.GetString("username");

            HttpContext.Session.Clear();


            return RedirectToAction("Login");
        }
    }
}
