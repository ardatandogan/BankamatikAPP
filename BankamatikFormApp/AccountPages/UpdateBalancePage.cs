using Bankamatik.Business.Services;
using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.Entities;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class UpdateBalancePage : Form
    {
        public Account CurrentAccount { get; set; }
        public int? CurrentUserID { get; set; }  // Kullanıcı ID'si için property eklendi

        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());

        public UpdateBalancePage()
        {
            InitializeComponent();
        }

        private void UpdateBalancePage_Load(object sender, EventArgs e)
        {
            txtBalance.Text = CurrentAccount.Balance.ToString("0.00");
            txtID.Text = CurrentAccount.AccountID.ToString();
        }

        private void btnUpdateBalance_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtBalance.Text, out decimal newBalance))
            {
                if (newBalance < 0)
                {
                    MessageBox.Show("Balance cannot be negative.");
                    return;
                }

                try
                {
                    CurrentAccount.Balance = newBalance;
                    accountService.UpdateAccount(CurrentAccount);

                    // Log kaydı eklendi
                    logService.InsertLog(CurrentUserID, "UpdateBalance", $"AccountID {CurrentAccount.AccountID} balance updated to {newBalance}");

                    MessageBox.Show("Balance updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating balance: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric balance.");
            }
        }
    }
}
