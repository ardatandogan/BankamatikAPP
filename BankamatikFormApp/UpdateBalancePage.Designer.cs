namespace BankamatikFormApp
{
    partial class UpdateBalancePage
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
            btnUpdateBalance = new Button();
            lblID = new Label();
            txtID = new TextBox();
            lblBalance = new Label();
            txtBalance = new TextBox();
            SuspendLayout();
            // 
            // btnUpdateBalance
            // 
            btnUpdateBalance.Location = new Point(128, 134);
            btnUpdateBalance.Name = "btnUpdateBalance";
            btnUpdateBalance.Size = new Size(197, 29);
            btnUpdateBalance.TabIndex = 15;
            btnUpdateBalance.Text = "Update Balance";
            btnUpdateBalance.UseVisualStyleBackColor = true;
            btnUpdateBalance.Click += btnUpdateBalance_Click;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(98, 41);
            lblID.Name = "lblID";
            lblID.Size = new Size(24, 20);
            lblID.TabIndex = 14;
            lblID.Text = "ID";
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(128, 38);
            txtID.Name = "txtID";
            txtID.Size = new Size(197, 27);
            txtID.TabIndex = 13;
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(61, 94);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(61, 20);
            lblBalance.TabIndex = 10;
            lblBalance.Text = "Balance";
            // 
            // txtBalance
            // 
            txtBalance.Enabled = true;
            txtBalance.Location = new Point(128, 91);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(197, 27);
            txtBalance.TabIndex = 9;
            // 
            // UpdateBalancePage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 238);
            Controls.Add(btnUpdateBalance);
            Controls.Add(lblID);
            Controls.Add(txtID);
            Controls.Add(lblBalance);
            Controls.Add(txtBalance);
            Name = "UpdateBalancePage";
            Text = "UpdateBalancePage";
            Load += UpdateBalancePage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUpdateBalance;
        private Label lblID;
        private TextBox txtID;
        private Label lblPasswordHash;
        private TextBox txtPasswordHash;
        private Label lblBalance;
        private TextBox txtBalance;
    }
}