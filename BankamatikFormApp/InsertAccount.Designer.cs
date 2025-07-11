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
            txtUserID.Location = new Point(152, 68);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(198, 27);
            txtUserID.TabIndex = 0;
            // 
            // txtAccountID
            // 
            txtAccountID.Enabled = false;
            txtAccountID.Location = new Point(152, 113);
            txtAccountID.Name = "txtAccountID";
            txtAccountID.Size = new Size(198, 27);
            txtAccountID.TabIndex = 1;
            // 
            // txtAccountBalance
            // 
            txtAccountBalance.Location = new Point(152, 161);
            txtAccountBalance.Name = "txtAccountBalance";
            txtAccountBalance.Size = new Size(198, 27);
            txtAccountBalance.TabIndex = 2;
            // 
            // lbl_AccountID
            // 
            lbl_AccountID.AutoSize = true;
            lbl_AccountID.Location = new Point(64, 116);
            lbl_AccountID.Name = "lbl_AccountID";
            lbl_AccountID.Size = new Size(82, 20);
            lbl_AccountID.TabIndex = 3;
            lbl_AccountID.Text = "Account ID";
            // 
            // lbl_Balance
            // 
            lbl_Balance.AutoSize = true;
            lbl_Balance.Location = new Point(85, 164);
            lbl_Balance.Name = "lbl_Balance";
            lbl_Balance.Size = new Size(61, 20);
            lbl_Balance.TabIndex = 4;
            lbl_Balance.Text = "Balance";
            // 
            // lbl_ID
            // 
            lbl_ID.AutoSize = true;
            lbl_ID.Location = new Point(85, 75);
            lbl_ID.Name = "lbl_ID";
            lbl_ID.Size = new Size(53, 20);
            lbl_ID.TabIndex = 5;
            lbl_ID.Text = "UserID";
            // 
            // btn_InsertAccount
            // 
            btn_InsertAccount.Location = new Point(194, 263);
            btn_InsertAccount.Name = "btn_InsertAccount";
            btn_InsertAccount.Size = new Size(113, 29);
            btn_InsertAccount.TabIndex = 6;
            btn_InsertAccount.Text = "Insert Account";
            btn_InsertAccount.UseVisualStyleBackColor = true;
            btn_InsertAccount.Click += btn_InsertAccount_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(152, 214);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(198, 28);
            comboBox1.TabIndex = 7;
            // 
            // lbl_ParaCinsi
            // 
            lbl_ParaCinsi.AutoSize = true;
            lbl_ParaCinsi.Location = new Point(74, 217);
            lbl_ParaCinsi.Name = "lbl_ParaCinsi";
            lbl_ParaCinsi.Size = new Size(72, 20);
            lbl_ParaCinsi.TabIndex = 8;
            lbl_ParaCinsi.Text = "Para Cinsi";
            // 
            // InsertAccount
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 333);
            Controls.Add(lbl_ParaCinsi);
            Controls.Add(comboBox1);
            Controls.Add(btn_InsertAccount);
            Controls.Add(lbl_ID);
            Controls.Add(lbl_Balance);
            Controls.Add(lbl_AccountID);
            Controls.Add(txtAccountBalance);
            Controls.Add(txtAccountID);
            Controls.Add(txtUserID);
            Name = "InsertAccount";
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