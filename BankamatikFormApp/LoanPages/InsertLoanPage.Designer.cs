namespace BankamatikFormApp
{
    partial class InsertLoanPage
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
            cmbAccounts = new ComboBox();
            txtAmount = new TextBox();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            cmbInterestRateOffers = new ComboBox();
            btnApply = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // cmbAccounts
            // 
            cmbAccounts.FormattingEnabled = true;
            cmbAccounts.Location = new Point(97, 16);
            cmbAccounts.Margin = new Padding(3, 2, 3, 2);
            cmbAccounts.Name = "cmbAccounts";
            cmbAccounts.Size = new Size(291, 23);
            cmbAccounts.TabIndex = 0;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(97, 50);
            txtAmount.Margin = new Padding(3, 2, 3, 2);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(291, 23);
            txtAmount.TabIndex = 1;
            // 
            // dtpStartDate
            // 
            dtpStartDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.Location = new Point(97, 84);
            dtpStartDate.Margin = new Padding(3, 2, 3, 2);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(291, 23);
            dtpStartDate.TabIndex = 2;
            // 
            // dtpEndDate
            // 
            dtpEndDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpEndDate.Location = new Point(97, 118);
            dtpEndDate.Margin = new Padding(3, 2, 3, 2);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(291, 23);
            dtpEndDate.TabIndex = 3;
            // 
            // cmbInterestRateOffers
            // 
            cmbInterestRateOffers.FormattingEnabled = true;
            cmbInterestRateOffers.Location = new Point(97, 152);
            cmbInterestRateOffers.Margin = new Padding(3, 2, 3, 2);
            cmbInterestRateOffers.Name = "cmbInterestRateOffers";
            cmbInterestRateOffers.Size = new Size(291, 23);
            cmbInterestRateOffers.TabIndex = 4;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.Gainsboro;
            btnApply.FlatAppearance.BorderColor = Color.Gainsboro;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 12F);
            btnApply.Location = new Point(97, 179);
            btnApply.Margin = new Padding(3, 2, 3, 2);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(291, 38);
            btnApply.TabIndex = 5;
            btnApply.Text = "Apply Loan";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 19);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 6;
            label1.Text = "Accounts";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 90);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 7;
            label2.Text = "StartDate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 53);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 8;
            label3.Text = "Amount";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(40, 124);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 9;
            label4.Text = "EndDate";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 155);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 10;
            label5.Text = "Interest Rate";
            // 
            // InsertLoanPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 233);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnApply);
            Controls.Add(cmbInterestRateOffers);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(txtAmount);
            Controls.Add(cmbAccounts);
            Margin = new Padding(3, 2, 3, 2);
            Name = "InsertLoanPage";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ApplyLoanPage";
            Load += ApplyLoanPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbAccounts;
        private TextBox txtAmount;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private ComboBox cmbInterestRateOffers;
        private Button btnApply;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}