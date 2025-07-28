using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class UpdatePage : Form
    {
        public User CurrentUser { get; set; }

        private readonly LogService logService = new LogService(new LogRepository());
        private readonly UserService userService;

        public UpdatePage()
        {
            InitializeComponent();
            userService = new UserService(new UserRepository(), logService);
        }

        private void UpdatePage_Load(object sender, EventArgs e)
        {
            txtUsername.Text = CurrentUser.Username;
            txtPasswordHash.Text = CurrentUser.PasswordHash;
            txtID.Text = CurrentUser.ID.ToString();
        }

        private void btnUpdateUsername_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentUser.PasswordHash = txtPasswordHash.Text;
                userService.UpdateUser(CurrentUser);


                MessageBox.Show("User updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }
    }
}
