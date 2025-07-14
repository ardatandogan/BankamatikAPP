using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class InsertAccount : Form
    {
        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly LogService logService = new LogService(new LogRepository());

        public User? CurrentUser { get; set; }

        public InsertAccount()
        {
            InitializeComponent();
        }

        private void InsertAccount_Load(object sender, EventArgs e)
        {
            var paraCinsleri = new List<string>
    {
        "USD", "AUD", "DKK", "EUR", "GBP", "CHF", "SEK", "CAD", "KWD", "NOK", "SAR", "JPY",
        "BGN", "RON", "RUB", "CNY", "PKR", "QAR", "KRW", "AZN", "AED", "XDR"
    };
            comboBox1.DataSource = paraCinsleri;

            if (CurrentUser != null && CurrentUser.Role.Trim().ToLower() == "user")
            {
                txtUserID.Text = CurrentUser.ID.ToString();
                txtUserID.Enabled = false;

                txtAccountBalance.Enabled = true;
            }
            else
            {
                txtUserID.Enabled = true;
                txtAccountBalance.Enabled = true;
            }
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

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a currency.");
                return;
            }

            string paraCinsi = comboBox1.SelectedItem.ToString();

            var newAccount = new Account
            {
                UserID = userId,
                Balance = balance,
                ParaCinsi = paraCinsi,
                CreatedAt = DateTime.Now
            };

            try
            {
                accountService.CreateAccount(newAccount);

                // Log kaydı kaydedilip log tablosuna aktarılacak.
                logService.InsertLog(userId, "Create", $"New account created for user {userId} with balance {balance} and currency {paraCinsi}");

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
