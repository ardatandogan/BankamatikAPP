using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class InsertAccount : Form
    {
        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());

        public InsertAccount()
        {
            InitializeComponent();
        }

        private void InsertAccount_Load(object sender, EventArgs e)
        {
        }

        private void btn_InsertAccount_Click(object sender, EventArgs e)
        {
            string userIdText = txtUserID.Text.Trim();
            string balanceText = txtAccountBalance.Text.Trim();

            if (string.IsNullOrEmpty(userIdText) || string.IsNullOrEmpty(balanceText))
            {
                MessageBox.Show("UserID and Balance are required.");
                return;
            }

            if (!int.TryParse(userIdText, out int userId))
            {
                MessageBox.Show("UserID must be a valid integer.");
                return;
            }

            if (!decimal.TryParse(balanceText, out decimal balance))
            {
                MessageBox.Show("Balance must be a valid decimal number.");
                return;
            }

            if (balance < 0)
            {
                MessageBox.Show("Balance cannot be negative.");
                return;
            }

            var newAccount = new Account
            {
                UserID = userId,
                Balance = balance,
                CreatedAt = DateTime.Now
            };

            try
            {
                accountService.CreateAccount(newAccount);

                // Log kaydı
                logService.InsertLog(userId, "Create", $"New account created for user {userId} with balance {balance}");

                MessageBox.Show("Account inserted successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting account: {ex.Message}");
            }
        }
    }
}
