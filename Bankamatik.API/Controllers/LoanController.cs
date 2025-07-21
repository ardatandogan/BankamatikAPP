using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bankamatik.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _loanService;

        public LoanController()
        {
            _loanService = new LoanService(
                new DataAccess.Repositories.LoanRepository(),
                new LogService(new DataAccess.Repositories.LogRepository()),
                new DataAccess.Repositories.AccountRepository()
            );
        }

        // Yeni kredi başvurusu formu (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kredi başvurusu gönderme (POST)
        [HttpPost]
        public IActionResult Create(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return View(loan);
            }

            try
            {
                _loanService.InsertLoan(loan);  
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kredi oluşturma sırasında hata: " + ex.Message);
                return View(loan);
            }
        }

        // Kredi listesi sayfası
        public IActionResult Index()
        {
            var loans = _loanService.GetLoans(new Loan());
            return View(loans);
        }

    }
}
