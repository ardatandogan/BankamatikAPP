namespace BankamatikFormApp
{
    partial class DeleteTransaction
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
            btn_DeleteTransaction = new Button();
            lbl_TransactionID = new Label();
            txtTransactionID = new TextBox();
            SuspendLayout();
            // 
            // btn_DeleteTransaction
            // 
            btn_DeleteTransaction.Location = new Point(170, 151);
            btn_DeleteTransaction.Name = "btn_DeleteTransaction";
            btn_DeleteTransaction.Size = new Size(94, 29);
            btn_DeleteTransaction.TabIndex = 0;
            btn_DeleteTransaction.Text = "Delete Transaction";
            btn_DeleteTransaction.UseVisualStyleBackColor = true;
            btn_DeleteTransaction.Click += btn_DeleteTransaction_Click;
            // 
            // lbl_TransactionID
            // 
            lbl_TransactionID.AutoSize = true;
            lbl_TransactionID.Location = new Point(46, 109);
            lbl_TransactionID.Name = "lbl_TransactionID";
            lbl_TransactionID.Size = new Size(103, 20);
            lbl_TransactionID.TabIndex = 1;
            lbl_TransactionID.Text = "Transaction ID";
            // 
            // txtTransactionID
            // 
            txtTransactionID.Location = new Point(155, 106);
            txtTransactionID.Name = "txtTransactionID";
            txtTransactionID.Size = new Size(125, 27);
            txtTransactionID.TabIndex = 2;
            // 
            // DeleteTransaction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 314);
            Controls.Add(txtTransactionID);
            Controls.Add(lbl_TransactionID);
            Controls.Add(btn_DeleteTransaction);
            Name = "DeleteTransaction";
            Text = "DeleteTransaction";
            Load += DeleteTransaction_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_DeleteTransaction;
        private Label lbl_TransactionID;
        private TextBox txtTransactionID;
    }
}