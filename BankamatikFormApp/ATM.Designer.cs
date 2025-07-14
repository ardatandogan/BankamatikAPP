namespace BankamatikFormApp
{
    partial class ATM
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
            txtAccountID = new TextBox();
            txtAmount = new TextBox();
            btn_Deposit = new Button();
            lbl_AccountID = new Label();
            lbl_Amount = new Label();
            btn_Withdraw = new Button();
            SuspendLayout();
            // 
            // txtAccountID
            // 
            txtAccountID.Location = new Point(198, 115);
            txtAccountID.Name = "txtAccountID";
            txtAccountID.Size = new Size(218, 27);
            txtAccountID.TabIndex = 0;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(198, 182);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(218, 27);
            txtAmount.TabIndex = 1;
            // 
            // btn_Deposit
            // 
            btn_Deposit.Location = new Point(209, 240);
            btn_Deposit.Name = "btn_Deposit";
            btn_Deposit.Size = new Size(87, 29);
            btn_Deposit.TabIndex = 2;
            btn_Deposit.Text = "Deposit";
            btn_Deposit.UseVisualStyleBackColor = true;
            btn_Deposit.Click += btn_Deposit_Click;
            // 
            // lbl_AccountID
            // 
            lbl_AccountID.AutoSize = true;
            lbl_AccountID.Location = new Point(114, 118);
            lbl_AccountID.Name = "lbl_AccountID";
            lbl_AccountID.Size = new Size(78, 20);
            lbl_AccountID.TabIndex = 3;
            lbl_AccountID.Text = "AccountID";
            // 
            // lbl_Amount
            // 
            lbl_Amount.AutoSize = true;
            lbl_Amount.Location = new Point(130, 185);
            lbl_Amount.Name = "lbl_Amount";
            lbl_Amount.Size = new Size(62, 20);
            lbl_Amount.TabIndex = 4;
            lbl_Amount.Text = "Amount";
            // 
            // btn_Withdraw
            // 
            btn_Withdraw.Location = new Point(302, 240);
            btn_Withdraw.Name = "btn_Withdraw";
            btn_Withdraw.Size = new Size(94, 29);
            btn_Withdraw.TabIndex = 5;
            btn_Withdraw.Text = "Withdraw";
            btn_Withdraw.UseVisualStyleBackColor = true;
            btn_Withdraw.Click += btn_Withdraw_Click;
            // 
            // ATM
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 450);
            Controls.Add(btn_Withdraw);
            Controls.Add(lbl_Amount);
            Controls.Add(lbl_AccountID);
            Controls.Add(btn_Deposit);
            Controls.Add(txtAmount);
            Controls.Add(txtAccountID);
            Name = "ATM";
            Text = "ATM";
            Load += ATM_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAccountID;
        private TextBox txtAmount;
        private Button btn_Deposit;
        private Label lbl_AccountID;
        private Label lbl_Amount;
        private Button btn_Withdraw;
    }
}