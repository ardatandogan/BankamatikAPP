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

        private readonly TransactionService transactionService = new TransactionService(
            new TransactionRepository(), new AccountRepository());

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
            CurrentTransaction.Amount = decimal.TryParse(txtAmount.Text, out var amount) ? amount : 0;
            CurrentTransaction.TransactionDate = dateTimePicker1.Value;

            transactionService.UpdateTransaction(CurrentTransaction);
            MessageBox.Show("Transaction updated successfully.");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }
    }
}
