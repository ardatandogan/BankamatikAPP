namespace BankamatikFormApp
{
    partial class DeleteUser
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
            txtUserID = new TextBox();
            lbl_UserID = new Label();
            btn_DeleteUser = new Button();
            SuspendLayout();
            // 
            // txtUserID
            // 
            txtUserID.Location = new Point(148, 82);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(125, 27);
            txtUserID.TabIndex = 0;
            // 
            // lbl_UserID
            // 
            lbl_UserID.AutoSize = true;
            lbl_UserID.Location = new Point(89, 85);
            lbl_UserID.Name = "lbl_UserID";
            lbl_UserID.Size = new Size(53, 20);
            lbl_UserID.TabIndex = 1;
            lbl_UserID.Text = "UserID";
            // 
            // btn_DeleteUser
            // 
            btn_DeleteUser.Location = new Point(162, 126);
            btn_DeleteUser.Name = "btn_DeleteUser";
            btn_DeleteUser.Size = new Size(94, 29);
            btn_DeleteUser.TabIndex = 2;
            btn_DeleteUser.Text = "Delete User";
            btn_DeleteUser.UseVisualStyleBackColor = true;
            btn_DeleteUser.Click += btn_DeleteUser_Click;
            // 
            // DeleteUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 286);
            Controls.Add(btn_DeleteUser);
            Controls.Add(lbl_UserID);
            Controls.Add(txtUserID);
            Name = "DeleteUser";
            Text = "DeleteUser";
            Load += DeleteUser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserID;
        private Label lbl_UserID;
        private Button btn_DeleteUser;
    }
}