using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class ATM : Form
    {
        public User CurrentUser { get; set; }

        public ATM()
        {
            InitializeComponent();
        }
        private List<int> allowedAccountIds = new List<int>();
        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly TransactionService transactionService = new TransactionService(new TransactionRepository(), new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());
        private void ATM_Load(object sender, EventArgs e)
        {
            // Sadece kullanıcı kendi hesapları üzerinden işlem yapabilsin
            if (CurrentUser != null && CurrentUser.Role?.Trim().ToLower() == "user")
            {
                var userAccounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
                allowedAccountIds = userAccounts.Select(a => a.AccountID).ToList();
            }
        }

        private void HandleTransaction(bool isDeposit)
        {
            if (!int.TryParse(txtAccountID.Text.Trim(), out int accountId))
            {
                MessageBox.Show("Geçerli bir Account ID girin.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Geçerli bir miktar girin.");
                return;
            }

            if (!allowedAccountIds.Contains(accountId))
            {
                MessageBox.Show("Sadece kendi hesaplarınızla işlem yapabilirsiniz.");
                return;
            }

            decimal finalAmount = isDeposit ? amount : -amount;

            try
            {
                var transaction = new Transaction
                {
                    FromAccountID = accountId,
                    ToAccountID = accountId,
                    Amount = finalAmount,
                    TransactionDate = DateTime.Now
                };

                transactionService.CreateTransaction(transaction);

                logService.InsertLog(CurrentUser.ID, isDeposit ? "Deposit" : "Withdraw",
                    $"{accountId} hesabına işlem: {(isDeposit ? "+" : "-")}{amount}");

                MessageBox.Show(isDeposit ? "Para yatırma başarılı." : "Para çekme başarılı.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
    
        private void btn_Deposit_Click(object sender, EventArgs e)
        {
            HandleTransaction(isDeposit: true);

        }

        private void btn_Withdraw_Click(object sender, EventArgs e)
        {
            HandleTransaction(isDeposit: false);

        }
    }
}
