﻿using System;
using System.Collections.Generic;
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

        private readonly LogService logService = new LogService(new LogRepository());

        private readonly TransactionService transactionService;
        private readonly AccountService accountService;

        private List<int>? allowedAccountIds = new List<int>();

        public InsertTransactionPage()
        {
            InitializeComponent();

            accountService = new AccountService(new AccountRepository(), logService); 
            transactionService = new TransactionService(new TransactionRepository(), new AccountRepository(), logService);
        }

        private void InsertTransactionPage_Load(object sender, EventArgs e)
        {
            if (CurrentUser != null && CurrentUser.Role?.Trim().ToLower() == "user")
            {
                var userAccounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
                allowedAccountIds = userAccounts.Select(a => a.AccountID).ToList();
                comboBoxFromAccount.DataSource = allowedAccountIds;
            }
            else
            {
                allowedAccountIds = null;
                var allAccounts = accountService.GetAccountsByUserId(new Account()); // Tüm hesapları getiren metod
                var allAccountIds = allAccounts.Select(a => a.AccountID).ToList();
                comboBoxFromAccount.DataSource = allAccountIds;
            }
        }

        private void btn_InsertTransaction_Click(object sender, EventArgs e)
        {
            // Sınıf field'ı olan accountService'i kullanıyoruz, tekrar oluşturma
            if (!int.TryParse(comboBoxFromAccount.SelectedValue?.ToString(), out int fromAccountId))
            {
                MessageBox.Show("Please select a valid From Account.");
                return;
            }

            if (!int.TryParse(txtToAccountID.Text.Trim(), out int toAccountId))
            {
                MessageBox.Show("Please enter a valid To Account ID.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.");
                return;
            }

            var fromAccount = accountService.GetAccountsByUserId(new Account())
                                .FirstOrDefault(a => a.AccountID == fromAccountId);
            if (fromAccount == null)
            {
                MessageBox.Show("From Account not found.");
                return;
            }

            var toAccount = accountService.GetAccountsByUserId(new Account())
                                .FirstOrDefault(a => a.AccountID == toAccountId);
            if (toAccount == null)
            {
                MessageBox.Show("To Account not found.");
                return;
            }

            if (fromAccount.ParaCinsi != toAccount.ParaCinsi)
            {
                MessageBox.Show("From Account and To Account must have the same currency.");
                return;
            }

            if (fromAccount.Balance < amount)
            {
                MessageBox.Show("Insufficient balance in the From Account.");
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
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;

                accountService.UpdateAccount(fromAccount);
                accountService.UpdateAccount(toAccount);

                transactionService.CreateTransaction(newTransaction);

                MessageBox.Show("Transaction inserted successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting transaction: {ex.Message}");
            }
        }
    }
}
