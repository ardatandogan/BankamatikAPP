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

        private readonly AccountService accountService = new AccountService(new AccountRepository());
        private readonly KurService kurService = new KurService();
        private readonly LogService logService = new LogService(new LogRepository());


        public CurrencyBuySell()
        {
            InitializeComponent();
        }

        private void CurrencyBuySell_Load(object sender, EventArgs e)
        {
            try
            {
                var accounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });

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
            if (comboBoxFromAccount.SelectedItem == null || comboBoxToAccount.SelectedItem == null ||
                comboBoxCurrency.SelectedItem == null || !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount))
            {
                MessageBox.Show("Please select FromAccount, ToAccount, Currency and enter a valid amount.");
                return;
            }

            var fromAccount = ((AccountDisplayItem)comboBoxFromAccount.SelectedItem).Account;
            var toAccount = ((AccountDisplayItem)comboBoxToAccount.SelectedItem).Account;
            var currency = (Kur)comboBoxCurrency.SelectedItem;

            // Buy: fromAccount ParaCinsi "TRY" olmalı (TL'den dövize)
            if (fromAccount.ParaCinsi != "TRY")
            {
                MessageBox.Show("For buying currency, 'From Account' must be your TRY account.");
                return;
            }

            // ToAccount ParaCinsi seçilen döviz kodu olmalı
            if (toAccount.ParaCinsi != currency.Kod)
            {
                MessageBox.Show($"'To Account' must be a {currency.Kod} account.");
                return;
            }

            // Kullanıcı sadece kendi hesaplarını seçebilir (kontrol)
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

            fromAccount.Balance -= totalCost;
            toAccount.Balance += amount;

            accountService.UpdateAccount(fromAccount);
            accountService.UpdateAccount(toAccount);
            logService.InsertLog(CurrentUser.ID, "CurrencyBuy", $"Bought {amount} {currency.Kod} from AccountID {fromAccount.AccountID} to AccountID {toAccount.AccountID}");
            MessageBox.Show($"You bought {amount} {currency.Kod} for {totalCost} TRY.");
        }

        private void btn_Sell_Click(object sender, EventArgs e)
        {
            if (comboBoxFromAccount.SelectedItem == null || comboBoxToAccount.SelectedItem == null ||
                comboBoxCurrency.SelectedItem == null || !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount))
            {
                MessageBox.Show("Please select FromAccount, ToAccount, Currency and enter a valid amount.");
                return;
            }

            var fromAccount = ((AccountDisplayItem)comboBoxFromAccount.SelectedItem).Account;
            var toAccount = ((AccountDisplayItem)comboBoxToAccount.SelectedItem).Account;
            var currency = (Kur)comboBoxCurrency.SelectedItem;

            // Sell: fromAccount ParaCinsi seçilen döviz olmalı (dövizi satıyor)
            if (fromAccount.ParaCinsi != currency.Kod)
            {
                MessageBox.Show($"For selling, 'From Account' must be a {currency.Kod} account.");
                return;
            }

            // ToAccount ParaCinsi TRY olmalı (dövizi TL'ye satıyor)
            if (toAccount.ParaCinsi != "TRY")
            {
                MessageBox.Show("'To Account' must be your TRY account.");
                return;
            }

            // Kullanıcı sadece kendi hesaplarını seçebilir
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

            fromAccount.Balance -= amount;
            toAccount.Balance += totalReturn;

            accountService.UpdateAccount(fromAccount);
            accountService.UpdateAccount(toAccount);
            logService.InsertLog(CurrentUser.ID, "CurrencySell", $"Sold {amount} {currency.Kod} from AccountID {fromAccount.AccountID} to AccountID {toAccount.AccountID}");

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
