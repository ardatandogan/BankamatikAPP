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

        // 1. Geri uyumlu constructor (sadece LoanRepository)
        public LoanService(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
            _logService = new LogService(new LogRepository()); // Default log servisi
        }

        // 2. LogService ile kullanım
        public LoanService(LoanRepository loanRepository, LogService logService, AccountRepository accountRepository)
        {
            _loanRepository = loanRepository;
            _logService = logService;
            _accountRepository = accountRepository;
        }

        // 3. AccountRepository + LogService ile kullanım
        public LoanService(LoanRepository loanRepository, AccountRepository accountRepository, LogService logService)
        {
            _loanRepository = loanRepository;
            _accountRepository = accountRepository;
            _logService = logService;
        }

        public void InsertLoan(Loan loan)
        {
            _loanRepository.InsertLoan(loan);
            _logService?.InsertLog(loan.UserID, "InsertLoan",
                $"Loan inserted: Amount={loan.Amount}, InterestRate={loan.InterestRate}%, StartDate={loan.StartDate:yyyy-MM-dd}");
        }

        public void UpdateLoan(Loan loan)
        {
            _loanRepository.UpdateLoan(loan);
            _logService?.InsertLog(loan.UserID, "UpdateLoan",
                $"Loan updated: LoanID={loan.LoanID}, NewAmount={loan.Amount}, Status={loan.Status}");
        }

        public void DeleteLoan(Loan loan)
        {
            _loanRepository.DeleteLoan(loan);
            _logService?.InsertLog(loan.UserID, "DeleteLoan",
                $"Loan deleted: LoanID={loan.LoanID}");
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
