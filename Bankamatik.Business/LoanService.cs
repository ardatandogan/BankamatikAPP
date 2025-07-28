using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankamatik.Business.Services
{
    public class LoanService
    {
        private readonly LoanRepository _loanRepository;
        private readonly AccountRepository _accountRepository;
        private readonly LogService _logService;

        // Constructor: LoanRepository, AccountRepository ve LogService'i zorunlu alıyor
        public LoanService(LoanRepository loanRepository, AccountRepository accountRepository, LogService logService)
        {
            _loanRepository = loanRepository;
            _accountRepository = accountRepository;
            _logService = logService;
        }

        public void InsertLoan(Loan loan)
        {
            _loanRepository.InsertLoan(loan);

            _logService.InsertLog(new Log
            {
                UserID = loan.UserID,
                ActionType = "Insert",
                Description = $"Loan application created: Amount {loan.Amount}, Start {loan.StartDate.ToShortDateString()}, End {loan.EndDate.ToShortDateString()}",
                CreatedAt = DateTime.Now
            });
        }

        public void UpdateLoan(Loan loan)
        {
            _loanRepository.UpdateLoan(loan);

            _logService.InsertLog(new Log
            {
                UserID = loan.UserID,
                ActionType = "Update",
                Description = $"Loan updated: LoanID {loan.LoanID}, Status {loan.Status}",
                CreatedAt = DateTime.Now
            });
        }

        public void DeleteLoan(Loan loan)
        {
            _loanRepository.DeleteLoan(loan);

            _logService.InsertLog(new Log
            {
                UserID = loan.UserID,
                ActionType = "Delete",
                Description = $"Loan deleted: LoanID {loan.LoanID}",
                CreatedAt = DateTime.Now
            });
        }

        public List<Loan> GetLoans(Loan loan)
        {
            return _loanRepository.GetLoans(loan);
        }

        public List<Loan> GetActiveLoans(Loan loan)
        {
            List<Loan> allLoans = GetLoans(new Loan { UserID = loan.UserID });
            return allLoans.Where(l => l.Status == "Active").ToList();
        }

        public List<Loan> GetOverdueLoans(Loan loan)
        {
            List<Loan> loans = GetLoans(new Loan { UserID = loan.UserID });
            DateTime now = DateTime.Now;
            return loans.Where(l => l.EndDate < now && l.Status == "Active").ToList();
        }

        public decimal CalculateInterestAmount(Loan loan)
        {
            double totalYears = (loan.EndDate - loan.StartDate).TotalDays / 365.0;
            return loan.Amount * loan.InterestRate * (decimal)totalYears;
        }
    }
}
