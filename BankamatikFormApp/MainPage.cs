using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace BankamatikFormApp
{
    public partial class MainPage : Form
    {

        public User CurrentUser { get; set; }
        private GridTheme userTheme = GridTheme.Ice;


        AccountService accountService = new AccountService(new AccountRepository());
        UserService userService = new UserService(new UserRepository());
        TransactionService transactionService = new TransactionService(new TransactionRepository(), new AccountRepository());
        LogService logService = new LogService(new LogRepository());
        KurService kurService = new KurService();


        public MainPage()
        {

            InitializeComponent();

        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            if (CurrentUser == null)
            {
                textBox4.Text = "USER is null!";
                return;
            }
            LoadTransactionsGrid();

            textBox4.Text = $"USER: {CurrentUser.Username} ROLE: {CurrentUser.Role}";

            string? role = CurrentUser?.Role?.Trim().ToLower();

            if (role != "admin")
            {
                btn_ATM.Visible = true;

                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabLogs);

                btn_InsertUserPage.Visible = false;
                btn_DeleteUser.Visible = false;
                btn_DeleteAccount.Visible = false;
                btn_DeleteTransaction.Visible = false;

                textBox2.Enabled = false; // user search
                textBox3.Enabled = false; // transaction search
                txtUserID.Enabled = false; // log user id filter

                // Account filtre textboxı sadece kendi userID'si, değiştirilemez
                textBox1.Text = CurrentUser.ID.ToString();
                textBox1.Enabled = false;
            }
            else
            {
                btn_ATM.Visible = false;

                if (!tabControl1.TabPages.Contains(tabLogs))
                    tabControl1.TabPages.Add(tabLogs);

                btn_InsertUserPage.Visible = true;
                btn_DeleteUser.Visible = true;
                btn_DeleteAccount.Visible = true;
                btn_DeleteTransaction.Visible = true;

                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                txtUserID.Enabled = true;
            }

            LoadAccountsGrid();
            LoadTransactionsGrid();

            dgvUsers.DataSource = userService.GetAllUsers();
            dgvTransactions.Columns["AccountID"].Visible = false;
            dgv_Logs.DataSource = logService.GetLogsByFilters(new Log());

            if (userTheme != GridTheme.Default)
            {
                ApplyTheme(dgvAccounts, userTheme);
                ApplyTheme(dgvUsers, userTheme);
                ApplyTheme(dgvTransactions, userTheme);
                ApplyTheme(dgv_Logs, userTheme);
                ApplyTheme(dgvKurlar, userTheme);
            }

            textBox4.Text += " || LOGS: Loaded: " + (dgv_Logs.DataSource as List<Log>)?.Count;
            var kurListesi = kurService.KurlariGetir();
            dgvKurlar.DataSource = kurListesi;


            if (dgv_Logs.Columns.Contains("StartDate"))
                dgv_Logs.Columns["StartDate"].Visible = false;

            if (dgv_Logs.Columns.Contains("EndDate"))
                dgv_Logs.Columns["EndDate"].Visible = false;
        }

        private void LoadAccountsGrid()
        {
            if (CurrentUser == null) return;

            string role = CurrentUser.Role?.Trim().ToLower();

            if (role == "user")
            {
                dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account
                {
                    UserID = CurrentUser.ID
                });
            }
            else
            {
                dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account());
            }
        }

        private void LoadTransactionsGrid()
        {
            if (CurrentUser == null) return;

            string role = CurrentUser.Role?.Trim().ToLower();

            if (role == "user")
            {
                // User'ın sadece kendi hesaplarına ait transactionları getir
                var userAccounts = accountService.GetAccountsByUserId(new Account { UserID = CurrentUser.ID });
                var userAccountIds = userAccounts.Select(a => a.AccountID).ToList();

                var allTransactions = transactionService.GetTransactions(new Transaction());

                var filteredTransactions = allTransactions
                    .Where(t => userAccountIds.Contains(t.FromAccountID))
                    .ToList();

                dgvTransactions.DataSource = filteredTransactions;
            }
            else
            {
                // Admin ise tüm transactionları göster
                dgvTransactions.DataSource = transactionService.GetTransactions(new Transaction());
            }
        }




        #region Search TextBoxları
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvAccounts.DataSource = null;
            dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account()
            {
                UserID = string.IsNullOrEmpty(textBox1.Text) ? 0 : Convert.ToInt32(textBox1.Text)
            });
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


        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            var filteredLogs = logService.GetLogsByFilters(new Log()
            {
                UserID = string.IsNullOrEmpty(txtUserID.Text) ? 0 : Convert.ToInt32(txtUserID.Text)
            });


            dgv_Logs.DataSource = filteredLogs;
            dgv_Logs.Columns.Remove("StartDate");
            dgv_Logs.Columns.Remove("EndDate");
        }
        #endregion


        #region Double Click Event'leri

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
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

        #endregion


        #region ButtonClicks
        private void btn_InsertUser_Click(object sender, EventArgs e)
        {
            InsertUserPage insertUserPage = new InsertUserPage();
            insertUserPage.ShowDialog();
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = userService.GetAllUsers();
        }

        private void btn_InsertAccount_Click(object sender, EventArgs e)
        {
            InsertAccount insertAccount = new InsertAccount();
            insertAccount.CurrentUser = this.CurrentUser;

            insertAccount.ShowDialog();

            // Form kapandıktan sonra hesapları filtreli yükle
            LoadAccountsGrid();
        }



        private void btn_InsertTransactionPage(object sender, EventArgs e)
        {
            InsertTransactionPage insertTransactionPage = new InsertTransactionPage();
            insertTransactionPage.CurrentUser = this.CurrentUser;
            insertTransactionPage.ShowDialog();

            // Transactionları rol bazlı yükle
            LoadTransactionsGrid();
        }

        private void btn_DeleteAccount_Click(object sender, EventArgs e)
        {
            DeleteAccount deleteAccount = new DeleteAccount();
            deleteAccount.ShowDialog();

            dgvAccounts.DataSource = null;
            dgvAccounts.DataSource = accountService.GetAccountsByUserId(new Account());
            //LoadAccountsGrid();
        }

        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            deleteUser.ShowDialog();

            dgvUsers.DataSource = null;
            dgvUsers.DataSource = userService.GetAllUsers();
            
        }

        private void btn_DeleteTransaction_Click(object sender, EventArgs e)
        {
            DeleteTransaction deleteTransaction = new DeleteTransaction();
            deleteTransaction.ShowDialog();

            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = transactionService.GetTransactions(new Transaction());
        }

        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
                LogService logService = new LogService(new LogRepository());
                logService.InsertLog(CurrentUser.ID, "Logout", $"{CurrentUser.Username} has logged out.");
            }
        }

        private void btn_ATM_Click(object sender, EventArgs e)
        {
            ATM atmPage = new ATM();
            atmPage.CurrentUser = this.CurrentUser;
            atmPage.ShowDialog();
        }

        private void btn_CurrencyBuySell_Click(object sender, EventArgs e)
        {
            var currencyForm = new CurrencyBuySell
            {
                CurrentUser = this.CurrentUser  
            };

            currencyForm.ShowDialog();  
        }
        #endregion

        #region Exports
        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (dgv_Logs.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Logs");

                        // Kolon başlıkları
                        for (int i = 0; i < dgv_Logs.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgv_Logs.Columns[i].HeaderText;
                        }

                        // Satır verileri
                        for (int i = 0; i < dgv_Logs.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgv_Logs.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgv_Logs.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        try
                        {
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful.", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"File in use or permission denied.\n\n{ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btn_ExportAccounts_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Accounts");

                        // Kolon başlıkları
                        for (int i = 0; i < dgvAccounts.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvAccounts.Columns[i].HeaderText;
                        }

                        // Satır verileri
                        for (int i = 0; i < dgvAccounts.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvAccounts.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvAccounts.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        try
                        {
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful.", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"File in use or permission denied.\n\n{ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btn_ExportUsers_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Users");

                        // Kolon başlıkları
                        for (int i = 0; i < dgvUsers.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvUsers.Columns[i].HeaderText;
                        }

                        // Satır verileri
                        for (int i = 0; i < dgvUsers.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvUsers.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvUsers.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        try
                        {
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful.", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"File in use or permission denied.\n\n{ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btn_ExportTransactions_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Transactions");

                        // Kolon başlıkları
                        for (int i = 0; i < dgvTransactions.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvTransactions.Columns[i].HeaderText;
                        }

                        // Satır verileri
                        for (int i = 0; i < dgvTransactions.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvTransactions.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvTransactions.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        try
                        {
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful.", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"File in use or permission denied.\n\n{ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btn_ExportKurlar_Click(object sender, EventArgs e)
        {
            if (dgvKurlar.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Kurlar");

                        // Kolon başlıkları
                        for (int i = 0; i < dgvKurlar.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvKurlar.Columns[i].HeaderText;
                        }

                        // Satır verileri
                        for (int i = 0; i < dgvKurlar.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvKurlar.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvKurlar.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        try
                        {
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful.", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"File in use or permission denied.\n\n{ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        #endregion


        #region ThemeChange
        private enum GridTheme
        {
            Default,
            Ice,
            Dark,
            Neon,
            Flat
        }

        private void ApplyTheme(DataGridView dgv, GridTheme theme)
        {
            switch (theme)
            {
                case GridTheme.Ice:
                    ApplyIceStyleToGrid(dgv);
                    break;
                case GridTheme.Dark:
                    ApplyDarkStyleToGrid(dgv);
                    break;
                case GridTheme.Neon:
                    ApplyNeonStyleToGrid(dgv);
                    break;
                case GridTheme.Flat:
                    ApplyFlatStyleToGrid(dgv);
                    break;
            }
        }

        private void ApplyIceStyleToGrid(DataGridView dgv)
        {
            // Alternating rows (açık mavi)
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(227, 241, 251) // soft ice blue
            };

            // Column headers
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(195, 222, 245),
                Font = new Font("Segoe UI", 9.75F, FontStyle.Regular),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(61, 102, 241),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.EnableHeadersVisualStyles = false;

            // Default cells
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = SystemColors.Window,
                Font = new Font("Segoe UI", 9F),
                ForeColor = SystemColors.ControlText,
                Padding = new Padding(5),
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText,
                WrapMode = DataGridViewTriState.False
            };

            // Row headers
            dgv.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(44, 74, 117),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(61, 102, 241),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Genel ayarlar
            dgv.BackgroundColor = Color.FromArgb(245, 249, 251);
            dgv.GridColor = Color.FromArgb(186, 206, 209);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.RowTemplate.Height = 30;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ApplyFlatStyleToGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Gainsboro,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.LightBlue,
                SelectionForeColor = Color.Black,
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(4)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.WhiteSmoke
            };

            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 28;
        }

        private void ApplyNeonStyleToGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.Black;
            dgv.GridColor = Color.FromArgb(0, 255, 255); // Neon cyan

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 0, 64),
                ForeColor = Color.Lime,
                Font = new Font("Consolas", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Black,
                ForeColor = Color.Aqua,
                SelectionBackColor = Color.Fuchsia,
                SelectionForeColor = Color.White,
                Font = new Font("Consolas", 9F),
                Padding = new Padding(5)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(10, 10, 30)
            };

            dgv.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 0, 30),
                ForeColor = Color.Lime
            };

            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 30;
        }

        private void ApplyDarkStyleToGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgv.GridColor = Color.FromArgb(45, 45, 45);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(70, 130, 180),
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(5)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(40, 40, 40)
            };

            dgv.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 30;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userTheme = (GridTheme)comboBox1.SelectedIndex;
            MainPage_Load(sender, e);
        }

        #endregion







       

    }
}
