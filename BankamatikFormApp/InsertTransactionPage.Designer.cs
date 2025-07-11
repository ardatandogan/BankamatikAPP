namespace BankamatikFormApp
{
    partial class InsertTransactionPage
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
            txtFromAccountID = new TextBox();
            txtToAccountID = new TextBox();
            txtAmount = new TextBox();
            btn_InsertTransaction = new Button();
            lbl_FromAccountID = new Label();
            lbl_ToAccountID = new Label();
            lbl_Amount = new Label();
            SuspendLayout();
            // 
            // txtFromAccountID
            // 
            txtFromAccountID.Location = new Point(157, 62);
            txtFromAccountID.Name = "txtFromAccountID";
            txtFromAccountID.Size = new Size(164, 27);
            txtFromAccountID.TabIndex = 0;
            txtFromAccountID.Validating += txtFromAccountID_Validating;
            // 
            // txtToAccountID
            // 
            txtToAccountID.Location = new Point(157, 110);
            txtToAccountID.Name = "txtToAccountID";
            txtToAccountID.Size = new Size(164, 27);
            txtToAccountID.TabIndex = 1;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(157, 161);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(164, 27);
            txtAmount.TabIndex = 2;
            // 
            // btn_InsertTransaction
            // 
            btn_InsertTransaction.Location = new Point(172, 208);
            btn_InsertTransaction.Name = "btn_InsertTransaction";
            btn_InsertTransaction.Size = new Size(134, 29);
            btn_InsertTransaction.TabIndex = 3;
            btn_InsertTransaction.Text = "Insert Transaction";
            btn_InsertTransaction.UseVisualStyleBackColor = true;
            btn_InsertTransaction.Click += btn_InsertTransaction_Click;
            // 
            // lbl_FromAccountID
            // 
            lbl_FromAccountID.AutoSize = true;
            lbl_FromAccountID.Location = new Point(39, 65);
            lbl_FromAccountID.Name = "lbl_FromAccountID";
            lbl_FromAccountID.Size = new Size(112, 20);
            lbl_FromAccountID.TabIndex = 4;
            lbl_FromAccountID.Text = "FromAccountID";
            // 
            // lbl_ToAccountID
            // 
            lbl_ToAccountID.AutoSize = true;
            lbl_ToAccountID.Location = new Point(57, 113);
            lbl_ToAccountID.Name = "lbl_ToAccountID";
            lbl_ToAccountID.Size = new Size(94, 20);
            lbl_ToAccountID.TabIndex = 5;
            lbl_ToAccountID.Text = "ToAccountID";
            // 
            // lbl_Amount
            // 
            lbl_Amount.AutoSize = true;
            lbl_Amount.Location = new Point(89, 164);
            lbl_Amount.Name = "lbl_Amount";
            lbl_Amount.Size = new Size(62, 20);
            lbl_Amount.TabIndex = 6;
            lbl_Amount.Text = "Amount";
            // 
            // InsertTransactionPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 332);
            Controls.Add(lbl_Amount);
            Controls.Add(lbl_ToAccountID);
            Controls.Add(lbl_FromAccountID);
            Controls.Add(btn_InsertTransaction);
            Controls.Add(txtAmount);
            Controls.Add(txtToAccountID);
            Controls.Add(txtFromAccountID);
            Name = "InsertTransactionPage";
            Text = "InsertTransactionPage";
            Load += InsertTransactionPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFromAccountID;
        private TextBox txtToAccountID;
        private TextBox txtAmount;
        private Button btn_InsertTransaction;
        private Label lbl_FromAccountID;
        private Label label2;
        private Label lbl_Amount;
        internal Label lbl_ToAccountID;
    }
}