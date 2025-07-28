using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BankamatikFormApp.LoanPages
{
    public partial class PayLoanPage : Form
    {
        private readonly LoanService loanService;
        private readonly AccountService accountService;
        private readonly LogService logService;

        public User CurrentUser { get; set; }

        public PayLoanPage()
        {
            InitializeComponent();

            // Doğru şekilde servisleri başlat
            loanService = new LoanService(
                new LoanRepository(),
                new AccountRepository(),
                new LogService(new LogRepository())
            );

            accountService = new AccountService(new AccountRepository(), logService);
            logService = new LogService(new LogRepository());
        }

        private void PayLoanPage_Load(object sender, EventArgs e)
        {
            LoadLoans();
            LoadAccounts();
        }

        private void btn_PayLoan_Click(object sender, EventArgs e)
        {
            if (comboBoxLoans.SelectedItem == null || comboBoxAccounts.SelectedItem == null)
            {
                MessageBox.Show("Lütfen hem kredi hem de hesap seçiniz.");
                return;
            }

            Loan selectedLoan = (Loan)comboBoxLoans.SelectedItem;
            Account selectedAccount = (Account)comboBoxAccounts.SelectedItem;

            if (selectedLoan.Status == "Paid")
            {
                MessageBox.Show("Bu kredi zaten ödenmiş.");
                return;
            }

            decimal interestAmount = loanService.CalculateInterestAmount(selectedLoan);
            decimal totalPayment = selectedLoan.Amount + interestAmount;

            if (selectedAccount.Balance < totalPayment)
            {
                MessageBox.Show("Hesap bakiyesi yetersiz.");
                return;
            }

            selectedAccount.Balance -= totalPayment;
            accountService.UpdateAccount(selectedAccount);

            selectedLoan.Status = "Paid";
            loanService.UpdateLoan(selectedLoan);

            MessageBox.Show($"Kredi ödendi. Toplam ödeme: {totalPayment} ₺");
            LoadLoans();
            LoadAccounts();
        }

        private void LoadLoans()
        {
            List<Loan> loans = (CurrentUser.Role == "Admin")
                ? loanService.GetLoans(new Loan())
                : loanService.GetActiveLoans(new Loan { UserID = CurrentUser.ID });

            comboBoxLoans.DataSource = loans;
            comboBoxLoans.DisplayMember = "LoanID";
            comboBoxLoans.ValueMember = "LoanID";
        }

        private void LoadAccounts()
        {
            List<Account> accounts = (CurrentUser.Role == "Admin")
                ? accountService.GetAccountsByUserId(new Account())
                : accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });

            comboBoxAccounts.DataSource = accounts;
            comboBoxAccounts.DisplayMember = "Display"; // Display adında property varsa
            comboBoxAccounts.ValueMember = "AccountID";
        }
    }
}
