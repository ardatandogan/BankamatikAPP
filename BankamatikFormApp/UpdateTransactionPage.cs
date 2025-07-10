using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class UpdateTransactionPage : Form
    {
        public Transaction CurrentTransaction { get; set; }
        public int? CurrentUserID { get; set; }  // Kullanıcı ID'si için property

        private readonly TransactionService transactionService = new TransactionService(
            new TransactionRepository(), new AccountRepository());

        private readonly LogService logService = new LogService(new LogRepository());

        public UpdateTransactionPage()
        {
            InitializeComponent();
        }

        private void UpdateTransactionPage_Load(object sender, EventArgs e)
        {
            txtTransactionID.Text = CurrentTransaction.TransactionID.ToString();
            txtAmount.Text = CurrentTransaction.Amount.ToString("0.00");
        }

        private void btn_UpdateTransaction_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out var amount))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            try
            {
                CurrentTransaction.Amount = amount;
                CurrentTransaction.TransactionDate = dateTimePicker1.Value;

                transactionService.UpdateTransaction(CurrentTransaction);

                // Log kaydı
                logService.InsertLog(CurrentUserID, "UpdateTransaction", $"Transaction ID {CurrentTransaction.TransactionID} updated. New amount: {amount}");

                MessageBox.Show("Transaction updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating transaction: {ex.Message}");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Şimdilik boş, gerekirse ekleme yapabilirsin
        }
    }
}
