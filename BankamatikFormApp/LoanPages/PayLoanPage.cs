using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankamatikFormApp.LoanPages

    
{
    public partial class PayLoanPage : Form
    {

        private readonly LoanService loanService = new LoanService(new LoanRepository());
        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());

        public User CurrentUser { get; set; }
        public PayLoanPage()
        {
            InitializeComponent();
        }

        private void btn_PayLoan_Click(object sender, EventArgs e)
        {
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

                // Toplam ödeme: anapara + faiz
                decimal totalPayment = selectedLoan.Amount + interestAmount;

                // Bakiye kontrolü
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


        }

        private void PayLoanPage_Load(object sender, EventArgs e)
        {
            LoadLoans();
            LoadAccounts();
        }

        private void LoadLoans()
        {
            List<Loan> loans;

            if (CurrentUser.Role == "Admin")
                loans = loanService.GetLoans(new Loan()); // tüm krediler
            else
                loans = loanService.GetActiveLoans(new Loan { UserID = CurrentUser.ID }); // kullanıcıya ait krediler

            comboBoxLoans.DataSource = loans;
            comboBoxLoans.DisplayMember = "LoanID";
            comboBoxLoans.ValueMember = "LoanID";
        }

        private void LoadAccounts()
        {
            List<Account> accounts;

            if (CurrentUser.Role == "Admin")
                accounts = accountService.GetAccountsByUserId(new Account ()); // varsa tüm hesapları getir
            else
                accounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });

            comboBoxAccounts.DataSource = accounts;
            comboBoxAccounts.DisplayMember = "ToString";
            comboBoxAccounts.ValueMember = "AccountID";
        }
    }
}
