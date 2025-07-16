namespace BankamatikFormApp
{
    partial class CurrencyBuySell
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
            comboBoxCurrency = new ComboBox();
            txtAmount = new TextBox();
            btn_Buy = new Button();
            btn_Sell = new Button();
            label1 = new Label();
            Amount = new Label();
            comboBoxFromAccount = new ComboBox();
            comboBoxToAccount = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // comboBoxCurrency
            // 
            comboBoxCurrency.FormattingEnabled = true;
            comboBoxCurrency.Location = new Point(171, 23);
            comboBoxCurrency.Name = "comboBoxCurrency";
            comboBoxCurrency.Size = new Size(158, 28);
            comboBoxCurrency.TabIndex = 0;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(171, 180);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(158, 27);
            txtAmount.TabIndex = 2;
            // 
            // btn_Buy
            // 
            btn_Buy.Location = new Point(171, 232);
            btn_Buy.Name = "btn_Buy";
            btn_Buy.Size = new Size(74, 40);
            btn_Buy.TabIndex = 3;
            btn_Buy.Text = "Buy";
            btn_Buy.UseVisualStyleBackColor = true;
            btn_Buy.Click += btn_Buy_Click;
            // 
            // btn_Sell
            // 
            btn_Sell.Location = new Point(251, 232);
            btn_Sell.Name = "btn_Sell";
            btn_Sell.Size = new Size(78, 40);
            btn_Sell.TabIndex = 4;
            btn_Sell.Text = "Sell";
            btn_Sell.UseVisualStyleBackColor = true;
            btn_Sell.Click += btn_Sell_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 23);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 5;
            label1.Text = "Currency";
            // 
            // Amount
            // 
            Amount.AutoSize = true;
            Amount.Location = new Point(100, 180);
            Amount.Name = "Amount";
            Amount.Size = new Size(62, 20);
            Amount.TabIndex = 7;
            Amount.Text = "Amount";
            // 
            // comboBoxFromAccount
            // 
            comboBoxFromAccount.FormattingEnabled = true;
            comboBoxFromAccount.Location = new Point(171, 77);
            comboBoxFromAccount.Name = "comboBoxFromAccount";
            comboBoxFromAccount.Size = new Size(158, 28);
            comboBoxFromAccount.TabIndex = 8;
            // 
            // comboBoxToAccount
            // 
            comboBoxToAccount.FormattingEnabled = true;
            comboBoxToAccount.Location = new Point(171, 129);
            comboBoxToAccount.Name = "comboBoxToAccount";
            comboBoxToAccount.Size = new Size(158, 28);
            comboBoxToAccount.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 80);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 10;
            label2.Text = "FromAccount";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(83, 132);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 11;
            label3.Text = "ToAccount";
            // 
            // CurrencyBuySell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 369);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBoxToAccount);
            Controls.Add(comboBoxFromAccount);
            Controls.Add(Amount);
            Controls.Add(label1);
            Controls.Add(btn_Sell);
            Controls.Add(btn_Buy);
            Controls.Add(txtAmount);
            Controls.Add(comboBoxCurrency);
            Name = "CurrencyBuySell";
            Text = "CurrencyBuySell";
            Load += CurrencyBuySell_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxCurrency;
        private TextBox txtAmount;
        private Button btn_Buy;
        private Button btn_Sell;
        private Label label1;
        private Label Amount;
        private ComboBox comboBoxFromAccount;
        private ComboBox comboBoxToAccount;
        private Label label2;
        private Label label3;
    }
}