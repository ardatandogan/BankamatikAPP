namespace BankamatikFormApp
{
    partial class InsertAccount
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
            txtUserID = new TextBox();
            txtAccountID = new TextBox();
            txtAccountBalance = new TextBox();
            lbl_AccountID = new Label();
            lbl_Balance = new Label();
            lbl_ID = new Label();
            btn_InsertAccount = new Button();
            comboBox1 = new ComboBox();
            lbl_ParaCinsi = new Label();
            SuspendLayout();
            // 
            // txtUserID
            // 
            txtUserID.Location = new Point(133, 51);
            txtUserID.Margin = new Padding(3, 2, 3, 2);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(174, 23);
            txtUserID.TabIndex = 0;
            // 
            // txtAccountID
            // 
            txtAccountID.Enabled = false;
            txtAccountID.Location = new Point(133, 85);
            txtAccountID.Margin = new Padding(3, 2, 3, 2);
            txtAccountID.Name = "txtAccountID";
            txtAccountID.Size = new Size(174, 23);
            txtAccountID.TabIndex = 1;
            // 
            // txtAccountBalance
            // 
            txtAccountBalance.Location = new Point(133, 121);
            txtAccountBalance.Margin = new Padding(3, 2, 3, 2);
            txtAccountBalance.Name = "txtAccountBalance";
            txtAccountBalance.Size = new Size(174, 23);
            txtAccountBalance.TabIndex = 2;
            // 
            // lbl_AccountID
            // 
            lbl_AccountID.AutoSize = true;
            lbl_AccountID.Location = new Point(56, 87);
            lbl_AccountID.Name = "lbl_AccountID";
            lbl_AccountID.Size = new Size(66, 15);
            lbl_AccountID.TabIndex = 3;
            lbl_AccountID.Text = "Account ID";
            // 
            // lbl_Balance
            // 
            lbl_Balance.AutoSize = true;
            lbl_Balance.Location = new Point(74, 123);
            lbl_Balance.Name = "lbl_Balance";
            lbl_Balance.Size = new Size(48, 15);
            lbl_Balance.TabIndex = 4;
            lbl_Balance.Text = "Balance";
            // 
            // lbl_ID
            // 
            lbl_ID.AutoSize = true;
            lbl_ID.Location = new Point(74, 56);
            lbl_ID.Name = "lbl_ID";
            lbl_ID.Size = new Size(41, 15);
            lbl_ID.TabIndex = 5;
            lbl_ID.Text = "UserID";
            // 
            // btn_InsertAccount
            // 
            btn_InsertAccount.Location = new Point(170, 197);
            btn_InsertAccount.Margin = new Padding(3, 2, 3, 2);
            btn_InsertAccount.Name = "btn_InsertAccount";
            btn_InsertAccount.Size = new Size(99, 22);
            btn_InsertAccount.TabIndex = 6;
            btn_InsertAccount.Text = "Insert Account";
            btn_InsertAccount.UseVisualStyleBackColor = true;
            btn_InsertAccount.Click += btn_InsertAccount_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(133, 160);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(174, 23);
            comboBox1.TabIndex = 7;
            // 
            // lbl_ParaCinsi
            // 
            lbl_ParaCinsi.AutoSize = true;
            lbl_ParaCinsi.Location = new Point(65, 163);
            lbl_ParaCinsi.Name = "lbl_ParaCinsi";
            lbl_ParaCinsi.Size = new Size(59, 15);
            lbl_ParaCinsi.TabIndex = 8;
            lbl_ParaCinsi.Text = "Para Cinsi";
            // 
            // InsertAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 250);
            Controls.Add(lbl_ParaCinsi);
            Controls.Add(comboBox1);
            Controls.Add(btn_InsertAccount);
            Controls.Add(lbl_ID);
            Controls.Add(lbl_Balance);
            Controls.Add(lbl_AccountID);
            Controls.Add(txtAccountBalance);
            Controls.Add(txtAccountID);
            Controls.Add(txtUserID);
            Margin = new Padding(3, 2, 3, 2);
            Name = "InsertAccount";
            StartPosition = FormStartPosition.CenterParent;
            Text = "InsertAccount";
            Load += InsertAccount_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserID;
        private TextBox txtAccountID;
        private TextBox txtAccountBalance;
        private Label lbl_AccountID;
        private Label lbl_Balance;
        private Label lbl_ID;
        private Button btn_InsertAccount;
        private ComboBox comboBox1;
        private Label lbl_ParaCinsi;
    }
}