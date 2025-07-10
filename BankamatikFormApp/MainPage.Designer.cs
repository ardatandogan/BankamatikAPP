namespace BankamatikFormApp
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvAccounts = new DataGridView();
            textBox1 = new TextBox();
            dgvUsers = new DataGridView();
            textBox3 = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            btn_ExportAccounts = new Button();
            lbl_Theme = new Label();
            btn_LogOut = new Button();
            comboBox1 = new ComboBox();
            btn_DeleteAccount = new Button();
            btn_InsertAccount = new Button();
            tabPage2 = new TabPage();
            splitContainer2 = new SplitContainer();
            btn_ExportUsers = new Button();
            btn_DeleteUser = new Button();
            btn_InsertUserPage = new Button();
            textBox2 = new TextBox();
            tabPage3 = new TabPage();
            splitContainer3 = new SplitContainer();
            btn_ExportTransactions = new Button();
            btn_DeleteTransaction = new Button();
            btn_InsertTransaction = new Button();
            dgvTransactions = new DataGridView();
            tabLogs = new TabPage();
            splitContainer5 = new SplitContainer();
            btn_Export = new Button();
            dgv_Logs = new DataGridView();
            splitContainer4 = new SplitContainer();
            textBox4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).BeginInit();
            tabLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Logs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            SuspendLayout();
            // 
            // dgvAccounts
            // 
            dgvAccounts.ColumnHeadersHeight = 29;
            dgvAccounts.Dock = DockStyle.Fill;
            dgvAccounts.Location = new Point(0, 0);
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.RowHeadersWidth = 51;
            dgvAccounts.Size = new Size(1244, 557);
            dgvAccounts.TabIndex = 4;
            dgvAccounts.DoubleClick += dgv_AccountsDoubleClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(5, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(377, 27);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dgvUsers
            // 
            dgvUsers.BackgroundColor = SystemColors.ActiveBorder;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.GridColor = SystemColors.HotTrack;
            dgvUsers.Location = new Point(0, 0);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(1244, 556);
            dgvUsers.TabIndex = 7;
            dgvUsers.DoubleClick += dgvUsers_DoubleClick;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(5, 21);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(374, 27);
            textBox3.TabIndex = 8;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabLogs);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1258, 664);
            tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1250, 631);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Accounts";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btn_ExportAccounts);
            splitContainer1.Panel1.Controls.Add(lbl_Theme);
            splitContainer1.Panel1.Controls.Add(btn_LogOut);
            splitContainer1.Panel1.Controls.Add(comboBox1);
            splitContainer1.Panel1.Controls.Add(btn_DeleteAccount);
            splitContainer1.Panel1.Controls.Add(btn_InsertAccount);
            splitContainer1.Panel1.Controls.Add(textBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvAccounts);
            splitContainer1.Size = new Size(1244, 625);
            splitContainer1.SplitterDistance = 64;
            splitContainer1.TabIndex = 6;
            // 
            // btn_ExportAccounts
            // 
            btn_ExportAccounts.Location = new Point(676, 24);
            btn_ExportAccounts.Name = "btn_ExportAccounts";
            btn_ExportAccounts.Size = new Size(116, 27);
            btn_ExportAccounts.TabIndex = 12;
            btn_ExportAccounts.Text = "Export to Excel";
            btn_ExportAccounts.UseVisualStyleBackColor = true;
            btn_ExportAccounts.Click += btn_ExportAccounts_Click;
            // 
            // lbl_Theme
            // 
            lbl_Theme.AutoSize = true;
            lbl_Theme.BackColor = Color.Transparent;
            lbl_Theme.BorderStyle = BorderStyle.Fixed3D;
            lbl_Theme.Location = new Point(835, 26);
            lbl_Theme.Name = "lbl_Theme";
            lbl_Theme.Size = new Size(56, 22);
            lbl_Theme.TabIndex = 11;
            lbl_Theme.Text = "Theme";
            // 
            // btn_LogOut
            // 
            btn_LogOut.BackColor = Color.Transparent;
            btn_LogOut.Location = new Point(1139, 4);
            btn_LogOut.Margin = new Padding(3, 4, 3, 4);
            btn_LogOut.Name = "btn_LogOut";
            btn_LogOut.Size = new Size(100, 27);
            btn_LogOut.TabIndex = 10;
            btn_LogOut.Text = "Logout";
            btn_LogOut.UseVisualStyleBackColor = false;
            btn_LogOut.Click += btn_LogOut_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Default", "Ice", "Dark", "Neon", "Flat" });
            comboBox1.Location = new Point(895, 23);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(221, 28);
            comboBox1.TabIndex = 9;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btn_DeleteAccount
            // 
            btn_DeleteAccount.Location = new Point(537, 23);
            btn_DeleteAccount.Name = "btn_DeleteAccount";
            btn_DeleteAccount.Size = new Size(120, 28);
            btn_DeleteAccount.TabIndex = 7;
            btn_DeleteAccount.Text = "Delete Account";
            btn_DeleteAccount.UseVisualStyleBackColor = true;
            btn_DeleteAccount.Click += btn_DeleteAccount_Click;
            // 
            // btn_InsertAccount
            // 
            btn_InsertAccount.Location = new Point(401, 23);
            btn_InsertAccount.Name = "btn_InsertAccount";
            btn_InsertAccount.Size = new Size(118, 28);
            btn_InsertAccount.TabIndex = 6;
            btn_InsertAccount.Text = "Insert Account";
            btn_InsertAccount.UseVisualStyleBackColor = true;
            btn_InsertAccount.Click += btn_InsertAccount_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(splitContainer2);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1250, 631);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Users";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(btn_ExportUsers);
            splitContainer2.Panel1.Controls.Add(btn_DeleteUser);
            splitContainer2.Panel1.Controls.Add(btn_InsertUserPage);
            splitContainer2.Panel1.Controls.Add(textBox2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dgvUsers);
            splitContainer2.Size = new Size(1244, 625);
            splitContainer2.SplitterDistance = 65;
            splitContainer2.TabIndex = 9;
            // 
            // btn_ExportUsers
            // 
            btn_ExportUsers.Location = new Point(652, 23);
            btn_ExportUsers.Name = "btn_ExportUsers";
            btn_ExportUsers.Size = new Size(131, 29);
            btn_ExportUsers.TabIndex = 11;
            btn_ExportUsers.Text = "Export To Excel";
            btn_ExportUsers.UseVisualStyleBackColor = true;
            btn_ExportUsers.Click += btn_ExportUsers_Click;
            // 
            // btn_DeleteUser
            // 
            btn_DeleteUser.Location = new Point(520, 23);
            btn_DeleteUser.Name = "btn_DeleteUser";
            btn_DeleteUser.Size = new Size(111, 29);
            btn_DeleteUser.TabIndex = 10;
            btn_DeleteUser.Text = "Delete User";
            btn_DeleteUser.UseVisualStyleBackColor = true;
            btn_DeleteUser.Click += btn_DeleteUser_Click;
            // 
            // btn_InsertUserPage
            // 
            btn_InsertUserPage.Location = new Point(401, 23);
            btn_InsertUserPage.Name = "btn_InsertUserPage";
            btn_InsertUserPage.Size = new Size(104, 29);
            btn_InsertUserPage.TabIndex = 9;
            btn_InsertUserPage.Text = "Insert User";
            btn_InsertUserPage.UseVisualStyleBackColor = true;
            btn_InsertUserPage.Click += btn_InsertUser_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(5, 23);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(375, 27);
            textBox2.TabIndex = 8;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(splitContainer3);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1250, 631);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Transactions";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(btn_ExportTransactions);
            splitContainer3.Panel1.Controls.Add(btn_DeleteTransaction);
            splitContainer3.Panel1.Controls.Add(btn_InsertTransaction);
            splitContainer3.Panel1.Controls.Add(textBox3);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(dgvTransactions);
            splitContainer3.Size = new Size(1244, 625);
            splitContainer3.SplitterDistance = 63;
            splitContainer3.TabIndex = 11;
            // 
            // btn_ExportTransactions
            // 
            btn_ExportTransactions.Location = new Point(711, 21);
            btn_ExportTransactions.Name = "btn_ExportTransactions";
            btn_ExportTransactions.Size = new Size(130, 31);
            btn_ExportTransactions.TabIndex = 11;
            btn_ExportTransactions.Text = "Export To Excel";
            btn_ExportTransactions.UseVisualStyleBackColor = true;
            btn_ExportTransactions.Click += btn_ExportTransactions_Click;
            // 
            // btn_DeleteTransaction
            // 
            btn_DeleteTransaction.Location = new Point(559, 21);
            btn_DeleteTransaction.Name = "btn_DeleteTransaction";
            btn_DeleteTransaction.Size = new Size(130, 31);
            btn_DeleteTransaction.TabIndex = 10;
            btn_DeleteTransaction.Text = "Delete Transaction";
            btn_DeleteTransaction.UseVisualStyleBackColor = true;
            btn_DeleteTransaction.Click += btn_DeleteTransaction_Click;
            // 
            // btn_InsertTransaction
            // 
            btn_InsertTransaction.Location = new Point(401, 21);
            btn_InsertTransaction.Name = "btn_InsertTransaction";
            btn_InsertTransaction.Size = new Size(138, 31);
            btn_InsertTransaction.TabIndex = 9;
            btn_InsertTransaction.Text = "Insert Transaction";
            btn_InsertTransaction.UseVisualStyleBackColor = true;
            btn_InsertTransaction.Click += btn_InsertTransactionPage;
            // 
            // dgvTransactions
            // 
            dgvTransactions.BackgroundColor = SystemColors.ActiveBorder;
            dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransactions.Dock = DockStyle.Fill;
            dgvTransactions.GridColor = SystemColors.HotTrack;
            dgvTransactions.Location = new Point(0, 0);
            dgvTransactions.Name = "dgvTransactions";
            dgvTransactions.ReadOnly = true;
            dgvTransactions.RowHeadersWidth = 51;
            dgvTransactions.Size = new Size(1244, 558);
            dgvTransactions.TabIndex = 10;
            dgvTransactions.DoubleClick += dgvTransactions_DoubleClick;
            // 
            // tabLogs
            // 
            tabLogs.Controls.Add(splitContainer5);
            tabLogs.Location = new Point(4, 29);
            tabLogs.Name = "tabLogs";
            tabLogs.Size = new Size(1250, 631);
            tabLogs.TabIndex = 3;
            tabLogs.Text = "Logs";
            tabLogs.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.Location = new Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Orientation = Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(btn_Export);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(dgv_Logs);
            splitContainer5.Size = new Size(1250, 631);
            splitContainer5.SplitterDistance = 47;
            splitContainer5.TabIndex = 1;
            // 
            // btn_Export
            // 
            btn_Export.Location = new Point(8, 9);
            btn_Export.Name = "btn_Export";
            btn_Export.Size = new Size(148, 35);
            btn_Export.TabIndex = 0;
            btn_Export.Text = "Export To Excel";
            btn_Export.UseVisualStyleBackColor = true;
            btn_Export.Click += btn_Export_Click;
            // 
            // dgv_Logs
            // 
            dgv_Logs.AllowUserToAddRows = false;
            dgv_Logs.AllowUserToDeleteRows = false;
            dgv_Logs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Logs.Dock = DockStyle.Fill;
            dgv_Logs.Location = new Point(0, 0);
            dgv_Logs.Name = "dgv_Logs";
            dgv_Logs.ReadOnly = true;
            dgv_Logs.RowHeadersWidth = 51;
            dgv_Logs.Size = new Size(1250, 580);
            dgv_Logs.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(textBox4);
            splitContainer4.Size = new Size(1258, 703);
            splitContainer4.SplitterDistance = 664;
            splitContainer4.TabIndex = 0;
            // 
            // textBox4
            // 
            textBox4.Dock = DockStyle.Fill;
            textBox4.Location = new Point(0, 0);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(1258, 27);
            textBox4.TabIndex = 0;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 703);
            Controls.Add(splitContainer4);
            Name = "MainPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainPage";
            Load += MainPage_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).EndInit();
            tabLogs.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Logs).EndInit();
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvAccounts;
        private TextBox textBox1;
        private DataGridView dgvUsers;
        private TextBox textBox3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox textBox2;
        private DataGridView dgvTransactions;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private Button btn_InsertUserPage;
        private Button btn_InsertAccount;
        private Button btn_InsertTransaction;
        private Button btn_DeleteAccount;
        private Button btn_DeleteUser;
        private Button btn_DeleteTransaction;
        private TabPage tabLogs;
        private DataGridView dgv_Logs;
        private SplitContainer splitContainer4;
        private ComboBox comboBox1;
        private TextBox textBox4;
        private Button btn_LogOut;
        private Label lbl_Theme;
        private SplitContainer splitContainer5;
        private Button btn_Export;
        private Button btn_ExportAccounts;
        private Button btn_ExportUsers;
        private Button btn_ExportTransactions;
    }
}