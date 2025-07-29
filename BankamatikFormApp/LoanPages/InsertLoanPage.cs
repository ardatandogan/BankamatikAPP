using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class InsertLoanPage : Form
    {
        public User CurrentUser { get; set; }

        private readonly AccountService accountService = new AccountService(
            new AccountRepository(),
            new LogService(new LogRepository())
        );

        private readonly LoanService loanService = new LoanService(
            new LoanRepository(),
            new AccountRepository(),
            new LogService(new LogRepository())
        );

        public InsertLoanPage()
        {
            InitializeComponent();
        }

        private void ApplyLoanPage_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            LoadInterestRateOffers();

            cmbAccounts.DisplayMember = "AccountDisplay";  
            cmbAccounts.ValueMember = "AccountID";

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddMonths(6);
        }

        private void LoadAccounts()
        {
            if (CurrentUser.Role == "Admin")
            {
                cmbAccounts.DataSource = accountService.GetAccountsByUserId(new Account());
            }
            else
            {
                cmbAccounts.DataSource = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
            }

            cmbAccounts.DisplayMember = "AccountDisplay"; 
            cmbAccounts.ValueMember = "AccountID";
        }

        private void LoadInterestRateOffers()
        {
            var accounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
            decimal totalBalance = accounts.Sum(a => a.Balance);

            List<decimal> offers;

            if (totalBalance > 1000000)
                offers = new List<decimal> { 0.10m };
            else if (totalBalance > 100000)
                offers = new List<decimal> { 0.15m };
            else
                offers = new List<decimal> { 0.20m };

            cmbInterestRateOffers.DataSource = offers;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Enter a valid loan amount.");
                    return;
                }

                if (cmbAccounts.SelectedValue == null || cmbInterestRateOffers.SelectedItem == null)
                {
                    MessageBox.Show("Please select both account and interest rate.");
                    return;
                }

                // Overdue kredi kontrolü
                var overdueLoans = loanService.GetOverdueLoans(new Loan { UserID = CurrentUser.ID });
                if (overdueLoans != null && overdueLoans.Any())
                {
                    MessageBox.Show("You have overdue loans. You cannot apply for a new loan until they are paid.");
                    return;
                }

                Loan loan = new Loan
                {
                    UserID = CurrentUser.ID,
                    Amount = amount,
                    InterestRate = (decimal)cmbInterestRateOffers.SelectedItem,
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value,
                    Status = "Active"
                };

                loanService.InsertLoan(loan);

                MessageBox.Show("Loan application submitted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}");
            }
        }
    }
}
