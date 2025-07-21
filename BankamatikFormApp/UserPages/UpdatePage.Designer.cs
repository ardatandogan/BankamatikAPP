namespace BankamatikFormApp
{
    partial class UpdatePage
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
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblPasswordHash = new Label();
            txtPasswordHash = new TextBox();
            lblID = new Label();
            txtID = new TextBox();
            btnUpdateUsername = new Button();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Enabled = false;
            txtUsername.Location = new Point(103, 42);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(197, 27);
            txtUsername.TabIndex = 0;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(17, 45);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // lblPasswordHash
            // 
            lblPasswordHash.AutoSize = true;
            lblPasswordHash.Location = new Point(22, 78);
            lblPasswordHash.Name = "lblPasswordHash";
            lblPasswordHash.Size = new Size(70, 20);
            lblPasswordHash.TabIndex = 3;
            lblPasswordHash.Text = "Password";
            // 
            // txtPasswordHash
            // 
            txtPasswordHash.Location = new Point(103, 75);
            txtPasswordHash.Name = "txtPasswordHash";
            txtPasswordHash.Size = new Size(197, 27);
            txtPasswordHash.TabIndex = 2;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(68, 111);
            lblID.Name = "lblID";
            lblID.Size = new Size(24, 20);
            lblID.TabIndex = 5;
            lblID.Text = "ID";
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(103, 108);
            txtID.Name = "txtID";
            txtID.Size = new Size(197, 27);
            txtID.TabIndex = 4;
            // 
            // btnUpdateUsername
            // 
            btnUpdateUsername.Location = new Point(103, 141);
            btnUpdateUsername.Name = "btnUpdateUsername";
            btnUpdateUsername.Size = new Size(197, 29);
            btnUpdateUsername.TabIndex = 8;
            btnUpdateUsername.Text = "Update";
            btnUpdateUsername.UseVisualStyleBackColor = true;
            btnUpdateUsername.Click += btnUpdateUsername_Click;
            // 
            // UpdatePage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 212);
            Controls.Add(btnUpdateUsername);
            Controls.Add(lblID);
            Controls.Add(txtID);
            Controls.Add(lblPasswordHash);
            Controls.Add(txtPasswordHash);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Name = "UpdatePage";
            Text = "UpdatePage";
            Load += UpdatePage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private Label lblUsername;
        private Label lblPasswordHash;
        private TextBox txtPasswordHash;
        private Label lblID;
        private TextBox txtID;
        private Button btnUpdateUsername;
    }
}