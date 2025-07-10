using System;
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
        private readonly LogService logService = new LogService(new LogRepository());

        public InsertTransactionPage()
        {
            InitializeComponent();
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

                // Log ekle
                logService.InsertLog(CurrentUser?.ID, "Create", $"Transaction inserted: From={fromAccountId}, To={toAccountId}, Amount={amount}");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
