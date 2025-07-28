using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class CurrencyBuySell : Form
    {
        public User CurrentUser { get; set; }

        private readonly AccountService accountService;
        private readonly LogService logService = new LogService(new LogRepository());
        private readonly KurService kurService = new KurService();

        public CurrencyBuySell()
        {
            InitializeComponent();
        }

        private void CurrencyBuySell_Load(object sender, EventArgs e)
        {
            try
            {
                List<Account>? accounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });

                // FromAccount combobox
                var fromList = accounts.Select(a => new AccountDisplayItem
                {
                    AccountID = a.AccountID,
                    DisplayText = $"{a.AccountID} ({a.ParaCinsi})",
                    Account = a
                }).ToList();

                comboBoxFromAccount.DataSource = new BindingSource(fromList, null);

                // ToAccount combobox
                var toList = accounts.Select(a => new AccountDisplayItem
                {
                    AccountID = a.AccountID,
                    DisplayText = $"{a.AccountID} ({a.ParaCinsi})",
                    Account = a
                }).ToList();

                comboBoxToAccount.DataSource = new BindingSource(toList, null);

                // Döviz kurları
                var kurlar = kurService.KurlariGetir();
                comboBoxCurrency.DataSource = kurlar;
                comboBoxCurrency.DisplayMember = "Kod";
                comboBoxCurrency.ValueMember = "Kod";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void btn_Buy_Click(object sender, EventArgs e)
        {
            const int BANK_USER_ID = 6006;

            if (comboBoxFromAccount.SelectedItem == null || comboBoxToAccount.SelectedItem == null ||
                comboBoxCurrency.SelectedItem == null || !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount))
            {
                MessageBox.Show("Please select FromAccount, ToAccount, Currency and enter a valid amount.");
                return;
            }

            var fromAccount = ((AccountDisplayItem)comboBoxFromAccount.SelectedItem).Account;
            var toAccount = ((AccountDisplayItem)comboBoxToAccount.SelectedItem).Account;
            var currency = (Kur)comboBoxCurrency.SelectedItem;

            if (fromAccount.ParaCinsi != "TRY")
            {
                MessageBox.Show("For buying currency, 'From Account' must be your TRY account.");
                return;
            }

            if (toAccount.ParaCinsi != currency.Kod)
            {
                MessageBox.Show($"'To Account' must be a {currency.Kod} account.");
                return;
            }

            if (fromAccount.UserID != CurrentUser.ID || toAccount.UserID != CurrentUser.ID)
            {
                MessageBox.Show("You can only select your own accounts.");
                return;
            }

            decimal totalCost = amount * currency.Alis;

            if (fromAccount.Balance < totalCost)
            {
                MessageBox.Show("Insufficient TRY balance.");
                return;
            }

            // Bankanın ilgili döviz hesabı bulunur
            var bankCurrencyAccount = accountService.GetAccountsByUserId(new Account())
                .FirstOrDefault(a => a.UserID == BANK_USER_ID && a.ParaCinsi == currency.Kod);

            if (bankCurrencyAccount == null || bankCurrencyAccount.Balance < amount)
            {
                MessageBox.Show("Bank does not have enough currency for this transaction.");
                return;
            }

            // Kullanıcı bakiyeleri güncelle
            fromAccount.Balance -= totalCost;
            toAccount.Balance += amount;
            accountService.UpdateAccount(fromAccount);
            accountService.UpdateAccount(toAccount);

            // Banka döviz bakiyesini düşür
            bankCurrencyAccount.Balance -= amount;
            accountService.UpdateAccount(bankCurrencyAccount);

            MessageBox.Show($"You bought {amount} {currency.Kod} for {totalCost} TRY.");
        }


        private void btn_Sell_Click(object sender, EventArgs e)
        {
            const int BANK_USER_ID = 6006;

            if (comboBoxFromAccount.SelectedItem == null || comboBoxToAccount.SelectedItem == null ||
                comboBoxCurrency.SelectedItem == null || !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount))
            {
                MessageBox.Show("Please select FromAccount, ToAccount, Currency and enter a valid amount.");
                return;
            }

            var fromAccount = ((AccountDisplayItem)comboBoxFromAccount.SelectedItem).Account;
            var toAccount = ((AccountDisplayItem)comboBoxToAccount.SelectedItem).Account;
            var currency = (Kur)comboBoxCurrency.SelectedItem;

            if (fromAccount.ParaCinsi != currency.Kod)
            {
                MessageBox.Show($"For selling, 'From Account' must be a {currency.Kod} account.");
                return;
            }

            if (toAccount.ParaCinsi != "TRY")
            {
                MessageBox.Show("'To Account' must be your TRY account.");
                return;
            }

            if (fromAccount.UserID != CurrentUser.ID || toAccount.UserID != CurrentUser.ID)
            {
                MessageBox.Show("You can only select your own accounts.");
                return;
            }

            if (fromAccount.Balance < amount)
            {
                MessageBox.Show("Insufficient foreign currency balance.");
                return;
            }

            decimal totalReturn = amount * currency.Satis;

            // Bankanın ilgili TRY ve döviz hesabı bulunur
            var bankCurrencyAccount = accountService.GetAccountsByUserId(new Account())
                .FirstOrDefault(a => a.UserID == BANK_USER_ID && a.ParaCinsi == currency.Kod);
            var bankTryAccount = accountService.GetAccountsByUserId(new Account())
                .FirstOrDefault(a => a.UserID == BANK_USER_ID && a.ParaCinsi == "TRY");

            if (bankCurrencyAccount == null || bankTryAccount == null || bankTryAccount.Balance < totalReturn)
            {
                MessageBox.Show("Bank cannot complete this transaction (not enough TRY).");
                return;
            }

            // Kullanıcı hesapları güncelle
            fromAccount.Balance -= amount;
            toAccount.Balance += totalReturn;
            accountService.UpdateAccount(fromAccount);
            accountService.UpdateAccount(toAccount);

            // Banka hesapları güncelle
            bankCurrencyAccount.Balance += amount;
            bankTryAccount.Balance -= totalReturn;
            accountService.UpdateAccount(bankCurrencyAccount);
            accountService.UpdateAccount(bankTryAccount);

            MessageBox.Show($"You sold {amount} {currency.Kod} and received {totalReturn} TRY.");
        }


        private class AccountDisplayItem
        {
            public int AccountID { get; set; }
            public string DisplayText { get; set; }
            public Account Account { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

    }
}
