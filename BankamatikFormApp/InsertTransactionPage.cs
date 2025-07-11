using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;

namespace BankamatikFormApp
{
    public partial class InsertTransactionPage : Form
    {

        public User? CurrentUser { get; set; }

        private readonly TransactionService transactionService = new TransactionService(new TransactionRepository(), new AccountRepository());
        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());
        private List<int>? allowedAccountIds = new List<int>();



        public InsertTransactionPage()
        {
            InitializeComponent();
            // Eğer Validating eventi eklemek istersen buraya ekleyebilirsin:
            // txtFromAccountID.Validating += TxtFromAccountID_Validating;
        }

        private void InsertTransactionPage_Load(object sender, EventArgs e)
        {
            if (CurrentUser != null && CurrentUser.Role?.Trim().ToLower() == "user")
            {
                var accountService = new AccountService(new AccountRepository()); // EKLE!
                var userAccounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
                allowedAccountIds = userAccounts.Select(a => a.AccountID).ToList();
            }
            else
            {
                allowedAccountIds = null; // Admin için kısıtlama yok
            }
        }


        private void btn_InsertTransaction_Click(object sender, EventArgs e)
        {
            string fromAccountIdText = txtFromAccountID.Text.Trim();
            string toAccountIdText = txtToAccountID.Text.Trim();
            string amountText = txtAmount.Text.Trim();

            if (string.IsNullOrEmpty(fromAccountIdText) || string.IsNullOrEmpty(toAccountIdText) || string.IsNullOrEmpty(amountText))
            {
                MessageBox.Show("FromAccountID, ToAccountID, and Amount are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(fromAccountIdText, out int fromAccountId))
            {
                MessageBox.Show("FromAccountID must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(toAccountIdText, out int toAccountId))
            {
                MessageBox.Show("ToAccountID must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Amount must be a valid decimal number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Amount must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kullanıcı rolü 'user' ise sadece kendi hesaplarından olan fromAccountId'yi kullanabilir
            if (CurrentUser != null && CurrentUser.Role?.Trim().ToLower() == "user" && allowedAccountIds != null)
            {
                if (!allowedAccountIds.Contains(fromAccountId))
                {
                    MessageBox.Show("You can only use your own Account IDs as FromAccountID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var newTransaction = new Transaction
            {
                FromAccountID = fromAccountId,
                ToAccountID = toAccountId,
                Amount = amount,
                TransactionDate = DateTime.Now
            };

            try
            {
                transactionService.CreateTransaction(newTransaction);
                MessageBox.Show("Transaction inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Log kaydı
                logService.InsertLog(CurrentUser?.ID, "Create", $"Transaction inserted: From={fromAccountId}, To={toAccountId}, Amount={amount}");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFromAccountID_Validating(object sender, CancelEventArgs e)
        {
            if (CurrentUser != null && CurrentUser.Role?.Trim().ToLower() == "user")
            {
                if (int.TryParse(txtFromAccountID.Text.Trim(), out int enteredId))
                {
                    if (!allowedAccountIds.Contains(enteredId))
                    {
                        e.Cancel = true;
                        MessageBox.Show("You are not allowed to use this Account ID. Please enter one of your own accounts.",
                            "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

       
    }
}
