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
        public int? CurrentUserID { get; set; }

        private readonly TransactionService transactionService;
        private readonly LogService logService;

        public UpdateTransactionPage()
        {
            InitializeComponent();

            // Servisler constructor içinde oluşturuluyor (logService önce!)
            logService = new LogService(new LogRepository());
            transactionService = new TransactionService(
                new TransactionRepository(),
                new AccountRepository(),
                logService);
        }

        private void UpdateTransactionPage_Load(object sender, EventArgs e)
        {
            if (CurrentTransaction == null)
            {
                MessageBox.Show("Transaction data not found.");
                Close();
                return;
            }

            txtTransactionID.Text = CurrentTransaction.TransactionID.ToString();
            txtAmount.Text = CurrentTransaction.Amount.ToString("0.00");
            dateTimePicker1.Value = CurrentTransaction.TransactionDate;
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

                MessageBox.Show("Transaction updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating transaction: {ex.Message}");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Gerekirse tarih değişimiyle ilgili işlem eklenebilir
        }
    }
}
