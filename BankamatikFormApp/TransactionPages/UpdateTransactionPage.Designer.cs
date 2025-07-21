namespace BankamatikFormApp
{
    partial class UpdateTransactionPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btn_UpdateTransaction = new Button();
            txtTransactionID = new TextBox();
            txtAmount = new TextBox();
            lbl_TransactionID = new Label();
            lbl_Amount = new Label();
            lbl_Date = new Label();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // btn_UpdateTransaction
            // 
            btn_UpdateTransaction.Location = new Point(235, 204);
            btn_UpdateTransaction.Name = "btn_UpdateTransaction";
            btn_UpdateTransaction.Size = new Size(150, 29);
            btn_UpdateTransaction.TabIndex = 0;
            btn_UpdateTransaction.Text = "Update Transaction";
            btn_UpdateTransaction.UseVisualStyleBackColor = true;
            btn_UpdateTransaction.Click += btn_UpdateTransaction_Click;
            // 
            // txtTransactionID
            // 
            txtTransactionID.Enabled = false;
            txtTransactionID.Location = new Point(197, 115);
            txtTransactionID.Name = "txtTransactionID";
            txtTransactionID.Size = new Size(250, 27);
            txtTransactionID.TabIndex = 1;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(197, 157);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(250, 27);
            txtAmount.TabIndex = 4;
            // 
            // lbl_TransactionID
            // 
            lbl_TransactionID.AutoSize = true;
            lbl_TransactionID.Location = new Point(88, 118);
            lbl_TransactionID.Name = "lbl_TransactionID";
            lbl_TransactionID.Size = new Size(103, 20);
            lbl_TransactionID.TabIndex = 5;
            lbl_TransactionID.Text = "Transaction ID";
            // 
            // lbl_Amount
            // 
            lbl_Amount.AutoSize = true;
            lbl_Amount.Location = new Point(129, 160);
            lbl_Amount.Name = "lbl_Amount";
            lbl_Amount.Size = new Size(62, 20);
            lbl_Amount.TabIndex = 8;
            lbl_Amount.Text = "Amount";
            // 
            // lbl_Date
            // 
            lbl_Date.AutoSize = true;
            lbl_Date.Location = new Point(150, 81);
            lbl_Date.Name = "lbl_Date";
            lbl_Date.Size = new Size(41, 20);
            lbl_Date.TabIndex = 10;
            lbl_Date.Text = "Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(197, 74);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 11;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // UpdateTransactionPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 300);
            Controls.Add(dateTimePicker1);
            Controls.Add(lbl_Date);
            Controls.Add(lbl_Amount);
            Controls.Add(lbl_TransactionID);
            Controls.Add(txtAmount);
            Controls.Add(txtTransactionID);
            Controls.Add(btn_UpdateTransaction);
            Name = "UpdateTransactionPage";
            Text = "Update Transaction";
            Load += UpdateTransactionPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_UpdateTransaction;
        private TextBox txtTransactionID;
        private TextBox txtToAccountID;
        private TextBox txtFromAccountID;
        private TextBox txtAmount;
        private Label lbl_TransactionID;
        private Label lbl_FromAccountID;
        private Label lbl_ToAccountID;
        private Label lbl_Amount;
        private TextBox txtDate;
        private Label lbl_Date;
        private DateTimePicker dateTimePicker1;
    }
}
