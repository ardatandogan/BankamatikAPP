using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankamatikWEBUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _loanService;
        private readonly AccountService _accountService;

        public LoanController()
        {
            var logRepository = new LogRepository();
            var logService = new LogService(logRepository);

            var loanRepository = new LoanRepository();
            var accountRepository = new AccountRepository();

            _loanService = new LoanService(loanRepository, accountRepository, logService);
            _accountService = new AccountService(accountRepository, logService);
        }

        // Ana sayfa - kredi listesi
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (!userId.HasValue)
                return RedirectToAction("Login", "Auth");

            List<Loan> loans;

            if (role.ToLower() == "admin")
            {
                loans = _loanService.GetLoans(new Loan());
            }
            else
            {
                loans = _loanService.GetLoans(new Loan { UserID = userId.Value });
            }

            return View(loans);
        }

        // GET: Kredi başvuru formu
        [HttpGet]
        public IActionResult ApplyLoan()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Auth");

            var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
            var totalBalance = accounts.Sum(a => a.Balance);

            List<decimal> interestOffers;
            if (totalBalance > 1_000_000) interestOffers = new List<decimal> { 0.10m };
            else if (totalBalance > 100_000) interestOffers = new List<decimal> { 0.15m };
            else interestOffers = new List<decimal> { 0.20m };

            ViewBag.InterestOffers = interestOffers;
            ViewBag.StartDate = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Today.AddMonths(6).ToString("yyyy-MM-dd");

            return View();
        }

        // POST: Kredi başvurusu işlemi (ViewModel yerine parametrelerle)
        [HttpPost]
        public IActionResult ApplyLoan(decimal amount, decimal interestRate, DateTime startDate, DateTime endDate)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Auth");

            if (amount <= 0)
                ModelState.AddModelError("amount", "Amount must be greater than zero.");

            if (startDate > endDate)
                ModelState.AddModelError("startDate", "Start date must be before end date.");

            if (!ModelState.IsValid)
            {
                // Faiz tekliflerini tekrar yükle
                var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
                var totalBalance = accounts.Sum(a => a.Balance);

                List<decimal> interestOffers;
                if (totalBalance > 1_000_000) interestOffers = new List<decimal> { 0.10m };
                else if (totalBalance > 100_000) interestOffers = new List<decimal> { 0.15m };
                else interestOffers = new List<decimal> { 0.20m };

                ViewBag.InterestOffers = interestOffers;
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

                return View();
            }

            // Gecikmiş kredi kontrolü
            var overdueLoans = _loanService.GetOverdueLoans(new Loan { UserID = userId.Value });
            if (overdueLoans != null && overdueLoans.Any())
            {
                ModelState.AddModelError("", "You have overdue loans. You cannot apply for a new loan until they are paid.");

                var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
                var totalBalance = accounts.Sum(a => a.Balance);

                List<decimal> interestOffers;
                if (totalBalance > 1_000_000) interestOffers = new List<decimal> { 0.10m };
                else if (totalBalance > 100_000) interestOffers = new List<decimal> { 0.15m };
                else interestOffers = new List<decimal> { 0.20m };

                ViewBag.InterestOffers = interestOffers;
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

                return View();
            }

            // Yeni kredi oluştur
            var loan = new Loan
            {
                UserID = userId.Value,
                Amount = amount,
                InterestRate = interestRate,
                StartDate = startDate,
                EndDate = endDate,
                Status = "Active"
            };

            _loanService.InsertLoan(loan);

            TempData["Success"] = "Loan application submitted successfully.";

            return RedirectToAction("Index");
        }

        // GET: Kredi ödeme sayfası (basit örnek)
        [HttpGet]
        public IActionResult PayLoan()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Auth");

            var loans = _loanService.GetActiveLoans(new Loan { UserID = userId.Value });
            var accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });

            ViewBag.Loans = loans;
            ViewBag.Accounts = accounts;

            return View();
        }

        // POST: Kredi ödeme işlemi (burayı ihtiyaçlarına göre düzenle)
        [HttpPost]
        public IActionResult PayLoan(int loanId, int accountId, decimal paymentAmount)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Auth");

            // Ödeme mantığı burada olacak, örn: bakiye kontrolü, kredi güncelleme vb.

            TempData["Success"] = "Loan payment processed.";

            return RedirectToAction("Index");
        }
    }
}
