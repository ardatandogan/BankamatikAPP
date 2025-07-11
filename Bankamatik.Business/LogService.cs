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

        // Tüm logları getirir
        public List<Log> GetLogsByFilters(Log log)
        {
            return _logRepository.GetLogsByFilters(log);
        }

        // Belirli bir kullanıcıya ait logları getirir
        

        // Yeni log kaydı ekler
        public void InsertLog(int? userId, string actionType, string description)
        {
            if (string.IsNullOrWhiteSpace(actionType))
                throw new ArgumentException("ActionType cannot be null or empty.", nameof(actionType));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            var log = new Log
            {
                UserID = userId,
                ActionType = actionType.Trim(),
                Description = description.Trim(),
                CreatedAt = DateTime.Now
            };

            _logRepository.InsertLog(log);
        }
    }
}
