using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using BankamatikWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BankamatikWebApp.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly KurService _kurService;
        private readonly AccountService _accountService;
        private readonly LogService _logService;

        public CurrencyController(KurService kurService, AccountService accountService, LogService logService)
        {
            _kurService = kurService;
            _accountService = accountService;
            _logService = logService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var kurlar = _kurService.KurlariGetir();
            return View(kurlar);
        }

        [HttpGet]
        public IActionResult Trade()
        {
            var model = new CurrencyBuySellViewModel();

            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";

            if (role.ToLower() != "admin" && userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            model.CurrentUserID = userId ?? 0;
            bool isAdmin = role.ToLower() == "admin";

            try
            {
                LoadAccounts(model, isAdmin, userId);
            }
            catch (Exception ex)
            {
                model.Message = $"Hesaplar yüklenirken hata oluştu: {ex.Message}";
                model.IsSuccess = false;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Buy(CurrencyBuySellViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var role = HttpContext.Session.GetString("role") ?? "";
            bool isAdmin = role.ToLower() == "admin";

            if (role.ToLower() != "admin" && userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            model.CurrentUserID = userId ?? 0;

            if (!ModelState.IsValid)
            {
                model.Message = "Lütfen tüm alanları doğru doldurun.";
                model.IsSuccess = false;
                LoadAccounts(model, isAdmin, userId);
                return View("Trade", model);
            }

            if (model.SelectedFromAccountId == model.SelectedToAccountId)
            {
                model.Message = "Gönderen ve alıcı hesaplar aynı olamaz.";
                model.IsSuccess = false;
                LoadAccounts(model, isAdmin, userId);
                return View("Trade", model);
            }

            decimal amountToDeduct = 0m;
            decimal amountToAdd = model.Amount;

            try
            {
                var userAccounts = _accountService.GetAccountsByUserId(new Account { UserID = userId.Value });

                var fromAccount = userAccounts.FirstOrDefault(a => a.AccountID == model.SelectedFromAccountId);
                var toAccount = userAccounts.FirstOrDefault(a => a.AccountID == model.SelectedToAccountId);

                if (fromAccount == null || toAccount == null)
                {
                    model.Message = "Hesaplar bulunamadı. Lütfen geçerli hesaplar seçiniz.";
                    model.IsSuccess = false;
                    LoadAccounts(model, isAdmin, userId);
                    return View("Trade", model);
                }

                if (!isAdmin && (fromAccount.UserID != userId.Value || toAccount.UserID != userId.Value))
                {
                    model.Message = "Yalnızca kendi hesaplarınızla işlem yapabilirsiniz.";
                    model.IsSuccess = false;
                    LoadAccounts(model, isAdmin, userId);
                    return View("Trade", model);
                }

                decimal calculatedExchangeRate = 1.0m;
                string transactionType = "";

                var currencyData = _kurService.KurlariGetir();

                // TRY'den Döviz Alımı
                if (fromAccount.ParaCinsi == "TRY" && toAccount.ParaCinsi != "TRY")
                {
                    var targetCurrency = currencyData.FirstOrDefault(k => k.Kod == toAccount.ParaCinsi);
                    if (targetCurrency == null) throw new Exception($"Hedef döviz kuru ({toAccount.ParaCinsi}) bulunamadı.");
                    calculatedExchangeRate = targetCurrency.Alis;

                    amountToDeduct = model.Amount * calculatedExchangeRate;
                    amountToAdd = model.Amount;
                    transactionType = $"Buy {toAccount.ParaCinsi} with TRY";
                }
                // Dövizden TRY Satımı
                else if (fromAccount.ParaCinsi != "TRY" && toAccount.ParaCinsi == "TRY")
                {
                    var sourceCurrency = currencyData.FirstOrDefault(k => k.Kod == fromAccount.ParaCinsi);
                    if (sourceCurrency == null) throw new Exception($"Kaynak döviz kuru ({fromAccount.ParaCinsi}) bulunamadı.");
                    calculatedExchangeRate = sourceCurrency.Satis;

                    amountToDeduct = model.Amount;
                    amountToAdd = model.Amount * calculatedExchangeRate;
                    transactionType = $"Sell {fromAccount.ParaCinsi} for TRY";
                }
                // Dövizden Dövize Takas
                else if (fromAccount.ParaCinsi != "TRY" && toAccount.ParaCinsi != "TRY")
                {
                    var fromCurrency = currencyData.FirstOrDefault(k => k.Kod == fromAccount.ParaCinsi);
                    var toCurrency = currencyData.FirstOrDefault(k => k.Kod == toAccount.ParaCinsi);

                    if (fromCurrency == null || toCurrency == null)
                    {
                        throw new Exception("Döviz kurları bulunamadı.");
                    }

                    decimal requiredTRYForTargetCurrency = model.Amount * toCurrency.Alis;
                    amountToDeduct = requiredTRYForTargetCurrency / fromCurrency.Satis;
                    amountToAdd = model.Amount;
                    transactionType = $"{fromAccount.ParaCinsi} to {toAccount.ParaCinsi}";
                }
                else if (fromAccount.ParaCinsi == toAccount.ParaCinsi)
                {
                    model.Message = "Aynı para birimine sahip hesaplar arasında döviz takası yapılamaz.";
                    model.IsSuccess = false;
                    LoadAccounts(model, isAdmin, userId);
                    return View("Trade", model);
                }
                else
                {
                    model.Message = "Geçersiz döviz takası işlemi. Desteklenmeyen döviz çifti.";
                    model.IsSuccess = false;
                    LoadAccounts(model, isAdmin, userId);
                    return View("Trade", model);
                }

                if (fromAccount.Balance < amountToDeduct)
                {
                    model.Message = $"Yetersiz {fromAccount.ParaCinsi} bakiyesi. Gereken: {amountToDeduct:N2} {fromAccount.ParaCinsi}";
                    model.IsSuccess = false;
                    LoadAccounts(model, isAdmin, userId);
                    return View("Trade", model);
                }

                // Hesap bakiyelerini güncelle
                fromAccount.Balance -= amountToDeduct;
                toAccount.Balance += amountToAdd;

                _accountService.UpdateAccount(fromAccount);
                _accountService.UpdateAccount(toAccount);

                // Log kaydı ekle
                _logService.InsertLog(new Log
                {
                    UserID = userId.Value,
                    ActionType = "CurrencyTrade",
                    Description = $"{transactionType}: {amountToDeduct:N2} {fromAccount.ParaCinsi} deducted, {amountToAdd:N2} {toAccount.ParaCinsi} added. From AccountID: {fromAccount.AccountID}, To AccountID: {toAccount.AccountID}",
                    CreatedAt = DateTime.Now
                });

                model.Message = $"{amountToDeduct:N2} {fromAccount.ParaCinsi} hesabınızdan düşüldü ve {amountToAdd:N2} {toAccount.ParaCinsi} hesabınıza eklendi.";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.Message = $"Döviz takası sırasında hata oluştu: {ex.Message}";
                model.IsSuccess = false;
            }

            LoadAccounts(model, isAdmin, userId);
            return View("Trade", model);
        }

        private void LoadAccounts(CurrencyBuySellViewModel model, bool isAdmin, int? currentUserId)
        {
            List<Account>? accounts;

            if (isAdmin)
            {
                accounts = _accountService.GetAccountsByUserId(new Account { UserID = null });
            }
            else
            {
                if (currentUserId.HasValue)
                {
                    accounts = _accountService.GetAccountsByUserId(new Account { UserID = currentUserId.Value });
                }
                else
                {
                    accounts = new List<Account>();
                }
            }

            model.FromAccounts = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountID.ToString(),
                Text = $"{a.AccountID} ({a.ParaCinsi}) - Bakiye: {a.Balance.ToString("N2")}",
                Selected = a.AccountID == model.SelectedFromAccountId
            }).ToList();

            model.ToAccounts = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountID.ToString(),
                Text = $"{a.AccountID} ({a.ParaCinsi}) - Bakiye: {a.Balance.ToString("N2")}",
                Selected = a.AccountID == model.SelectedToAccountId
            }).ToList();
        }
    }
}
