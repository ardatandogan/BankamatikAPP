namespace BankamatikFormApp.LoanPages
{
    partial class PayLoanPage
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
            comboBoxLoans = new ComboBox();
            comboBoxAccounts = new ComboBox();
            btn_PayLoan = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // comboBoxLoans
            // 
            comboBoxLoans.FormattingEnabled = true;
            comboBoxLoans.Location = new Point(153, 78);
            comboBoxLoans.Name = "comboBoxLoans";
            comboBoxLoans.Size = new Size(173, 23);
            comboBoxLoans.TabIndex = 0;
            // 
            // comboBoxAccounts
            // 
            comboBoxAccounts.FormattingEnabled = true;
            comboBoxAccounts.Location = new Point(153, 117);
            comboBoxAccounts.Name = "comboBoxAccounts";
            comboBoxAccounts.Size = new Size(173, 23);
            comboBoxAccounts.TabIndex = 1;
            // 
            // btn_PayLoan
            // 
            btn_PayLoan.Location = new Point(182, 165);
            btn_PayLoan.Name = "btn_PayLoan";
            btn_PayLoan.Size = new Size(118, 23);
            btn_PayLoan.TabIndex = 2;
            btn_PayLoan.Text = "Pay Loan";
            btn_PayLoan.UseVisualStyleBackColor = true;
            btn_PayLoan.Click += btn_PayLoan_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(77, 120);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 3;
            label1.Text = "ACCOUNTS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(102, 81);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 4;
            label2.Text = "LOANS";
            // 
            // PayLoanPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 363);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btn_PayLoan);
            Controls.Add(comboBoxAccounts);
            Controls.Add(comboBoxLoans);
            Name = "PayLoanPage";
            Text = "PayLoanPage";
            Load += PayLoanPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxLoans;
        private ComboBox comboBoxAccounts;
        private Button btn_PayLoan;
        private Label label1;
        private Label label2;
    }
}