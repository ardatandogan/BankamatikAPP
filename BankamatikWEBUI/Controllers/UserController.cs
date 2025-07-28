using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankamatikWEBUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            var userRepository = new UserRepository();
            var logRepository = new LogRepository();
            var logService = new LogService(logRepository);

            _userService = new UserService(userRepository, logService);
        }

        // Oturum kontrolü için yardımcı method
        private IActionResult CheckSession()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Auth");
            }
            return null;
        }

        #region Read

        public IActionResult Index(string search)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            var role = HttpContext.Session.GetString("role") ?? "";
            var username = HttpContext.Session.GetString("username") ?? "";

            List<User> users;

            if (role.ToLower() == "admin")
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var foundUser = _userService.GetUserByUsername(new User { Username = search });
                    users = foundUser != null ? new List<User> { foundUser } : new List<User>();
                }
                else
                {
                    users = _userService.GetAllUsers();
                }
            }
            else
            {
                // admin değilse sadece kendi kullanıcı bilgisi
                var currentUser = _userService.GetUserByUsername(new User { Username = username });
                users = currentUser != null ? new List<User> { currentUser } : new List<User>();
            }

            ViewBag.UserRole = role; // view için rolü gönder

            return View(users);
        }


        #endregion

        #region Insert

        public IActionResult Insert()
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost]
        public IActionResult Insert(User user)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            if (ModelState.IsValid)
            {
                string sonuc = _userService.CreateUser(user);
                if (sonuc != "success")
                {
                    return NotFound(sonuc);
                }

                return RedirectToAction("Index");
            }
            return View(user);
        }

        #endregion

        #region Update

        public IActionResult Update(int id)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            User? user = _userService.GetUserById(new User { ID = id });
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            if (ModelState.IsValid)
            {
                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;
             
            var user = _userService.GetUserById(new User { ID = id });
            if (user == null) return NotFound();

            return View(user);  // Confirm sayfası açılıyor
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var redirect = CheckSession();
            if (redirect != null) return redirect;

            _userService.DeleteUser(new User { ID = id });
            return RedirectToAction("Index");
        }

        #endregion
    }
}
