using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankamatikWEBUI.Controllers
{
    public class LogController : Controller
    {
        private readonly LogService _logService;

        public LogController()
        {
            _logService = new LogService(new Bankamatik.DataAccess.Repositories.LogRepository());
        }

        // Tüm logları listele
        public IActionResult Index()
        {
            List<Log> logs = _logService.GetLogsByFilters(new Log()); 
            return View(logs);
        }
    }
}
