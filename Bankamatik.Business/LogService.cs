using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace Bankamatik.Business.Services
{
    public class LogService
    {
        private readonly LogRepository _logRepository;

        public LogService(LogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public List<Log> GetLogsByFilters(Log log)
        {
            return _logRepository.GetLogsByFilters(log);
        }

        public void InsertLog(Log log)
        {
            _logRepository.InsertLog(log); // sadece log entity'si alıyor
        }
    }
}
