using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Identity.Client;

namespace BankamatikWEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService(new Bankamatik.DataAccess.Repositories.AccountRepository());
        }

        // Hesapları listele (admin ise tüm hesaplar, user ise kendi hesapları)
        public IActionResult Index(string? search)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
            {
                // Giriş yapılmamış kullanıcıyı login sayfasına yönlendir
                return RedirectToAction("Login", "Auth");
            }

            var filter = new Account();

            if (role.ToLower() == "admin")
            {
                // Admin tüm hesapları görebilir
                if (!string.IsNullOrEmpty(search))
                {
                    filter.UserID = int.Parse(search);
                }
                else
                {
                    filter.UserID = null; // tüm hesaplar
                }
            }
            else
            {
                // User sadece kendi hesaplarını görebilir
                filter.UserID = userId.Value;
                if (!string.IsNullOrEmpty(search))
                {
                    filter.UserID = int.Parse(search);
                }
            }

            var accounts = _accountService.GetAccountsByUserId(filter);
            ViewBag.Search = search;

            return View(accounts);
        }

        // Yeni hesap formu
        public IActionResult Insert()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        [HttpPost]
        public IActionResult Insert(Account account)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            account.UserID = userId.Value;

            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.CreateAccount(account);
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Hesap oluşturulurken hata oluştu: " + ex.Message);
                }
            }
            return View(account);
        }

        // Güncelleme formu (GET)
        public IActionResult Update(int id)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
                return RedirectToAction("Login", "Auth");

            // Hesabı getir
            Account? account = _accountService.GetAccountByAccountId(new Account { AccountID = id });
            if (account == null)
                return NotFound();

            // Eğer user ise sadece kendi hesabını güncelleyebilir
            if (role.ToLower() != "admin" && account.UserID != userId)
                return Unauthorized();

            return View(account);
        }

        // Güncelleme formu (POST)
        [HttpPost]
        public IActionResult Update(Account account)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
                return RedirectToAction("Login", "Auth");

            // User ise sadece kendi hesabını güncelleyebilir
            if (role.ToLower() != "admin" && userId != account.UserID)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.UpdateAccount(account);
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Güncelleme sırasında hata oluştu: " + ex.Message);
                }
            }
            return View(account);
        }

        // Silme onayı (GET)
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
                return RedirectToAction("Login", "Auth");

            var account = _accountService.GetAccountsByUserId(new Account { AccountID = id }).FirstOrDefault();
            if (account == null)
                return NotFound();

            if (role.ToLower() != "admin" && account.UserID != userId)
                return Unauthorized();

            return View(account);
        }

        // Silme işlemi (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
                return RedirectToAction("Login", "Auth");

            var account = _accountService.GetAccountsByUserId(new Account { AccountID = id }).FirstOrDefault();
            if (account == null)
                return NotFound();

            if (role.ToLower() != "admin" && account.UserID != userId)
                return Unauthorized();

            _accountService.DeleteAccount(id);
            return RedirectToAction("Index");
        }
    }
}
