namespace BankamatikFormApp
{
    partial class DeleteAccount
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
            btn_DeleteAccount = new Button();
            txtAccountID = new TextBox();
            lbl_AccountID = new Label();
            SuspendLayout();
            // 
            // btn_DeleteAccount
            // 
            btn_DeleteAccount.Location = new Point(126, 96);
            btn_DeleteAccount.Margin = new Padding(3, 2, 3, 2);
            btn_DeleteAccount.Name = "btn_DeleteAccount";
            btn_DeleteAccount.Size = new Size(104, 22);
            btn_DeleteAccount.TabIndex = 0;
            btn_DeleteAccount.Text = "Delete Account";
            btn_DeleteAccount.UseVisualStyleBackColor = true;
            btn_DeleteAccount.Click += btn_DeleteAccount_Click;
            // 
            // txtAccountID
            // 
            txtAccountID.Location = new Point(108, 62);
            txtAccountID.Margin = new Padding(3, 2, 3, 2);
            txtAccountID.Name = "txtAccountID";
            txtAccountID.Size = new Size(146, 23);
            txtAccountID.TabIndex = 1;
            // 
            // lbl_AccountID
            // 
            lbl_AccountID.AutoSize = true;
            lbl_AccountID.Location = new Point(34, 64);
            lbl_AccountID.Name = "lbl_AccountID";
            lbl_AccountID.Size = new Size(63, 15);
            lbl_AccountID.TabIndex = 2;
            lbl_AccountID.Text = "AccountID";
            // 
            // DeleteAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(343, 218);
            Controls.Add(lbl_AccountID);
            Controls.Add(txtAccountID);
            Controls.Add(btn_DeleteAccount);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DeleteAccount";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DeleteAccount";
            Load += DeleteAccount_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_DeleteAccount;
        private TextBox txtAccountID;
        private Label lbl_AccountID;
    }
}