using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BankamatikWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankamatikWebApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly AccountService _accountService;

        public TransactionController(TransactionService transactionService, AccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            string role = HttpContext.Session.GetString("role") ?? "";

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            List<Transaction> transactions;

            if (role.ToLower() == "admin")
            {
                transactions = _transactionService.GetTransactions(new Transaction());
            }
            else
            {
                transactions = _transactionService.GetTransactionsByUserId(userId.Value);
            }

            return View(transactions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            string role = HttpContext.Session.GetString("role") ?? "";

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            List<Account> fromAccounts;

            if (role.ToLower() == "admin")
            {
                fromAccounts = _accountService.GetAccountsByUserId(new Account());
            }
            else
            {
                fromAccounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
            }

            var allAccounts = _accountService.GetAccountsByUserId(new Account());

            ViewBag.FromAccounts = fromAccounts.Select(a => new
            {
                a.AccountID,
                Display = $"{a.AccountID} - {a.ParaCinsi}"
            }).ToList();

            ViewBag.ToAccounts = allAccounts.Select(a => new
            {
                a.AccountID,
                Display = $"{a.AccountID} - {a.ParaCinsi}"
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            try
            {
                _transactionService.CreateTransaction(transaction);
                TempData["SuccessMessage"] = "Transfer işlemi başarıyla tamamlandı.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Transfer sırasında beklenmedik bir hata oluştu: {ex.Message}");
            }

            // Hata olursa dropdownları tekrar yükle
            var userId = HttpContext.Session.GetInt32("userId");
            string role = HttpContext.Session.GetString("role") ?? "";
            List<Account> fromAccounts;
            if (role.ToLower() == "admin")
            {
                fromAccounts = _accountService.GetAccountsByUserId(new Account());
            }
            else
            {
                fromAccounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
            }
            var allAccounts = _accountService.GetAccountsByUserId(new Account());

            ViewBag.FromAccounts = fromAccounts.Select(a => new { a.AccountID, Display = $"{a.AccountID} - {a.ParaCinsi}" }).ToList();
            ViewBag.ToAccounts = allAccounts.Select(a => new { a.AccountID, Display = $"{a.AccountID} - {a.ParaCinsi}" }).ToList();

            return View(transaction);
        }

        public IActionResult Edit(int id)
        {
            var transaction = _transactionService.GetTransactionById(new Transaction { TransactionID = id });
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            try
            {
                _transactionService.UpdateTransaction(transaction);
                TempData["SuccessMessage"] = "İşlem başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(transaction);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int TransactionID)
        {
            var transaction = _transactionService.GetTransactionById(new Transaction { TransactionID = TransactionID });
            if (transaction == null)
                return NotFound();

            try
            {
                _transactionService.DeleteTransaction(transaction);
                TempData["SuccessMessage"] = "İşlem başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"İşlem silinirken hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult WithdrawDeposit()
        {
            var model = new WithdrawDepositViewModel();

            var userId = HttpContext.Session.GetInt32("userId");
            string role = HttpContext.Session.GetString("role") ?? "";

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            model.CurrentUserID = userId.Value;

            try
            {
                List<Account>? accounts;
                if (role.ToLower() == "admin")
                {
                    accounts = _accountService.GetAccountsByUserId(new Account());
                }
                else
                {
                    accounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });
                }

                model.Accounts = accounts.Select(a => new SelectListItem
                {
                    Value = a.AccountID.ToString(),
                    Text = $"{a.AccountID} ({a.ParaCinsi}) - Bakiye: {a.Balance.ToString("N2")}"
                }).ToList();

                if (!model.Accounts.Any())
                {
                    model.Message = "Hesap bilgileri yüklenemedi. Lütfen daha sonra tekrar deneyin.";
                    model.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                model.Message = $"Hesaplar yüklenirken hata oluştu: {ex.Message}";
                model.IsSuccess = false;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ProcessWithdrawDeposit(WithdrawDepositViewModel model, string actionType)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";
            bool isAdmin = role.ToLower() == "admin";

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            model.CurrentUserID = userId.Value;

            LoadAccountsForWithdrawDeposit(model, isAdmin, userId);

            if (!ModelState.IsValid)
            {
                model.Message = "Lütfen miktarı doğru giriniz.";
                model.IsSuccess = false;
                return View("WithdrawDeposit", model);
            }

            try
            {
                if (model.SelectedAccountId <= 0)
                {
                    model.Message = "Lütfen geçerli bir hesap seçiniz.";
                    model.IsSuccess = false;
                    return View("WithdrawDeposit", model);
                }

                Account? accountToUpdate;
                if (isAdmin)
                {
                    accountToUpdate = _accountService.GetAccountByAccountId(new Account { AccountID = model.SelectedAccountId });
                }
                else
                {
                    accountToUpdate = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value })
                                                     .FirstOrDefault(a => a.AccountID == model.SelectedAccountId);
                }

                if (accountToUpdate == null)
                {
                    model.Message = "Hesap bulunamadı veya yetkiniz yok.";
                    model.IsSuccess = false;
                    return View("WithdrawDeposit", model);
                }

                if (!isAdmin && accountToUpdate.UserID != userId.Value)
                {
                    model.Message = "Yalnızca kendi hesaplarınızla işlem yapabilirsiniz.";
                    model.IsSuccess = false;
                    return View("WithdrawDeposit", model);
                }

                if (actionType == "Withdraw")
                {
                    _transactionService.WithdrawMoney(model.SelectedAccountId, model.Amount, userId.Value);
                    model.Message = $"{model.Amount.ToString("N2")} {accountToUpdate.ParaCinsi} hesabınızdan başarıyla çekildi.";
                }
                else if (actionType == "Deposit")
                {
                    _transactionService.DepositMoney(model.SelectedAccountId, model.Amount, userId.Value);
                    model.Message = $"{model.Amount.ToString("N2")} {accountToUpdate.ParaCinsi} hesabınıza başarıyla yatırıldı.";
                }
                else
                {
                    model.Message = "Geçersiz işlem tipi.";
                    model.IsSuccess = false;
                    return View("WithdrawDeposit", model);
                }

                model.IsSuccess = true;
            }
            catch (InvalidOperationException ex)
            {
                model.Message = ex.Message;
                model.IsSuccess = false;
            }
            catch (ArgumentException ex)
            {
                model.Message = ex.Message;
                model.IsSuccess = false;
            }
            catch (Exception ex)
            {
                model.Message = $"İşlem sırasında beklenmedik bir hata oluştu: {ex.Message}";
                model.IsSuccess = false;
            }

            return View("WithdrawDeposit", model);
        }

        private void LoadAccountsForWithdrawDeposit(WithdrawDepositViewModel model, bool isAdmin, int? currentUserId)
        {
            List<Account>? accounts;

            if (isAdmin)
            {
                accounts = _accountService.GetAccountsByUserId(new Account());
            }
            else
            {
                if (currentUserId.HasValue)
                    accounts = _accountService.GetAccountsByUserId(new Account { UserID = currentUserId.Value });
                else
                    accounts = new List<Account>();
            }

            model.Accounts = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountID.ToString(),
                Text = $"{a.AccountID} ({a.ParaCinsi}) - Bakiye: {a.Balance.ToString("N2")}",
                Selected = a.AccountID == model.SelectedAccountId
            }).ToList();
        }
    }
}
