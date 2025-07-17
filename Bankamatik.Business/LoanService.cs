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

        public LoanService(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }


        public bool AddLoan(Loan loan)
        {
            return _loanRepository.InsertLoan(loan);
        }

        public bool UpdateLoan(Loan loan)
        {
            return _loanRepository.UpdateLoan(loan);
        }

        
        public bool DeleteLoan(int loanId)
        {
            return _loanRepository.DeleteLoan(loanId);
        }

 
        public List<Loan> GetLoans(Loan loan)
        {
            return _loanRepository.GetLoans(loan);
        }

        // suanki loanları listeye dönüştür
        public List<Loan> GetActiveLoans(Loan loan)
        {
            var allLoans = _loanRepository.GetLoans(new Loan { UserID = loan.UserID });
            return allLoans.Where(l => l.Status == "Active").ToList();
        }

        // enddatei gecmis kredisi aktif mi
        public bool HasOverdueLoans(Loan loan)
        {
            var loans = _loanRepository.GetLoans(new Loan { UserID = loan.UserID });
            var now = DateTime.Now;
            return loans.Any(l => l.EndDate < now && l.Status == "Active");
        }

        public decimal CalculateInterestAmount(Loan loan)
        {
            //kac yıl oldugunu hesaplıyor.
            double totalYears = (loan.EndDate - loan.StartDate).TotalDays / 365.0;
            return loan.Amount * loan.InterestRate * (decimal)totalYears;
        }

        // geri ödeme miktarı
        public decimal CalculateTotalRepayment(Loan loan)
        {
            return loan.Amount + CalculateInterestAmount(loan);
        }

        // paid ise:
        public bool MarkLoanAsPaid(int loanId)
        {
            return _loanRepository.UpdateLoanStatus(loanId, "Paid");
        }


    }
}
