namespace BankamatikFormApp
{
    partial class InsertUserPage
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
            txtID = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            lbl_ID = new Label();
            lbl_Username = new Label();
            lbl_PasswordHash = new Label();
            btn_InsertUser = new Button();
            SuspendLayout();
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(150, 113);
            txtID.Name = "txtID";
            txtID.Size = new Size(190, 27);
            txtID.TabIndex = 0;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(150, 159);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(190, 27);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(150, 208);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(190, 27);
            txtPassword.TabIndex = 2;
            // 
            // lbl_ID
            // 
            lbl_ID.AutoSize = true;
            lbl_ID.Location = new Point(120, 116);
            lbl_ID.Name = "lbl_ID";
            lbl_ID.Size = new Size(24, 20);
            lbl_ID.TabIndex = 4;
            lbl_ID.Text = "ID";
            // 
            // lbl_Username
            // 
            lbl_Username.AutoSize = true;
            lbl_Username.Location = new Point(69, 162);
            lbl_Username.Name = "lbl_Username";
            lbl_Username.Size = new Size(75, 20);
            lbl_Username.TabIndex = 5;
            lbl_Username.Text = "Username";
            // 
            // lbl_PasswordHash
            // 
            lbl_PasswordHash.AutoSize = true;
            lbl_PasswordHash.Location = new Point(74, 215);
            lbl_PasswordHash.Name = "lbl_PasswordHash";
            lbl_PasswordHash.Size = new Size(70, 20);
            lbl_PasswordHash.TabIndex = 6;
            lbl_PasswordHash.Text = "Password";
            // 
            // btn_InsertUser
            // 
            btn_InsertUser.Location = new Point(195, 255);
            btn_InsertUser.Name = "btn_InsertUser";
            btn_InsertUser.Size = new Size(94, 29);
            btn_InsertUser.TabIndex = 7;
            btn_InsertUser.Text = "Insert User";
            btn_InsertUser.UseVisualStyleBackColor = true;
            btn_InsertUser.Click += btn_InsertUser_Click;
            // 
            // InsertUserPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 365);
            Controls.Add(btn_InsertUser);
            Controls.Add(lbl_PasswordHash);
            Controls.Add(lbl_Username);
            Controls.Add(lbl_ID);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(txtID);
            Name = "InsertUserPage";
            Text = "InsertUserPage";
            Load += InsertUserPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtID;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lbl_ID;
        private Label lbl_Username;
        private Label lbl_PasswordHash;
        private Button btn_InsertUser;
    }
}