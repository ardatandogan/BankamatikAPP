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
            btn_DeleteAccount.Location = new Point(144, 128);
            btn_DeleteAccount.Name = "btn_DeleteAccount";
            btn_DeleteAccount.Size = new Size(119, 29);
            btn_DeleteAccount.TabIndex = 0;
            btn_DeleteAccount.Text = "Delete Account";
            btn_DeleteAccount.UseVisualStyleBackColor = true;
            btn_DeleteAccount.Click += btn_DeleteAccount_Click;
            // 
            // txtAccountID
            // 
            txtAccountID.Location = new Point(123, 82);
            txtAccountID.Name = "txtAccountID";
            txtAccountID.Size = new Size(166, 27);
            txtAccountID.TabIndex = 1;
            // 
            // lbl_AccountID
            // 
            lbl_AccountID.AutoSize = true;
            lbl_AccountID.Location = new Point(39, 85);
            lbl_AccountID.Name = "lbl_AccountID";
            lbl_AccountID.Size = new Size(78, 20);
            lbl_AccountID.TabIndex = 2;
            lbl_AccountID.Text = "AccountID";
            // 
            // DeleteAccount
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(392, 291);
            Controls.Add(lbl_AccountID);
            Controls.Add(txtAccountID);
            Controls.Add(btn_DeleteAccount);
            Name = "DeleteAccount";
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