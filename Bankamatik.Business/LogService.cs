using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;

namespace Bankamatik.Business.Services
{
    public class LogService
    {
        private readonly LogRepository _logRepository;

        public LogService(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public List<Log> GetAllLogs()
        {
            return _logRepository.GetAllLogs();
        }


        public void InsertLog(int? userId, string actionType, string description)
        {
            if (string.IsNullOrWhiteSpace(actionType))
                throw new ArgumentException("ActionType cannot be null or empty.");

            var log = new Log
            {
                UserID = userId,
                ActionType = actionType,
                Description = description,
                CreatedAt = DateTime.Now
            };

            _logRepository.InsertLog(log);
        }

    }
}
