using Bankamatik.Business;
using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class UpdatePage : Form
    {
        public User CurrentUser { get; set; }
        UserService userService = new UserService(new UserRepository());

        public UpdatePage()
        {
            InitializeComponent();
        }

        private void btnUpdateUsername_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentUser.PasswordHash = txtPasswordHash.Text;
                userService.UpdateUser(CurrentUser);

                // Log ekle
                LogService logService = new LogService(new LogRepository());
                logService.InsertLog(CurrentUser.ID, "Update", $"User updated: ID={CurrentUser.ID}, Username={CurrentUser.Username}");

                MessageBox.Show("User updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }

        private void UpdatePage_Load(object sender, EventArgs e)
        {
            txtUsername.Text = CurrentUser.Username;    
            txtPasswordHash.Text = CurrentUser.PasswordHash;    
            txtID.Text = CurrentUser.ID.ToString();    
        }
    }
}
