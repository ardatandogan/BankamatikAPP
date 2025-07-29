using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class DeleteAccount : Form
    {
        private readonly AccountService accountService;
        private readonly TransactionService transactionService;
        private readonly LogService logService = new LogService(new LogRepository());

        public User? CurrentUser { get; set; }

        public DeleteAccount()
        {
            InitializeComponent();
            accountService = new AccountService(new Bankamatik.DataAccess.Repositories.AccountRepository(), logService);
            transactionService = new TransactionService(
    new TransactionRepository(),
    new AccountRepository(),
    new LogService(new LogRepository())
);
        }

        private void btn_DeleteAccount_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAccountID.Text.Trim(), out int accountId))
            {
                try
                {
                    bool success = accountService.DeleteAccount(accountId);

                    if (success)
                    {
                        MessageBox.Show("Account and related transactions deleted successfully.");
                       
                    }
                    else
                    {
                        MessageBox.Show("Account not found or could not be deleted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting account: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Account ID.");
            }
        }

        private void DeleteAccount_Load(object sender, EventArgs e)
        {

        }
    }
}
