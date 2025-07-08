using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class MainPage : Form
    {
        AccountService accountService = new AccountService(new AccountRepository());
        UserService userService = new UserService(new UserRepository());
        TransactionService transactionService = new TransactionService(new TransactionRepository(), new AccountRepository());

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account());
            dgvUsers.DataSource = userService.GetAllUsers();
            dgvTransactions.DataSource = transactionService.GetTransactions(new Transaction());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvAccounts.DataSource = null;
            dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account() { UserID = string.IsNullOrEmpty(textBox1.Text) ? 0 : Convert.ToInt32(textBox1.Text) });
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox2.Text.Trim().ToLower();

            var allUsers = userService.GetAllUsers();

            var filteredUsers = allUsers
                .Where(u => u.Username.ToLower().Contains(keyword))
                .ToList();

            dgvUsers.DataSource = null;
            dgvUsers.DataSource = filteredUsers;
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int accountId = 0;
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
                int.TryParse(textBox3.Text, out accountId);

            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = transactionService.GetTransactions(
                new Transaction { AccountID = accountId == 0 ? null : accountId }
            );
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            //İlgili satır bilgileri UpdatePage'e aktarılır.
            UpdatePage updatePage = new UpdatePage();
            updatePage.CurrentUser = dgvUsers.CurrentRow.DataBoundItem as User;
            updatePage.ShowDialog();
        }


        private void dgv_AccountsDoubleClick(object sender, EventArgs e)
        {
            UpdateBalancePage updateBalancePage = new UpdateBalancePage();
            updateBalancePage.CurrentAccount = dgvAccounts.CurrentRow.DataBoundItem as Account;
            updateBalancePage.ShowDialog();
        }
        private void dgvTransactions_DoubleClick(object sender, EventArgs e)
        {
            UpdateTransactionPage updateTransactionPage = new UpdateTransactionPage();
            updateTransactionPage.CurrentTransaction = dgvTransactions.CurrentRow.DataBoundItem as Transaction;
            updateTransactionPage.ShowDialog();
            }
        }
    }

