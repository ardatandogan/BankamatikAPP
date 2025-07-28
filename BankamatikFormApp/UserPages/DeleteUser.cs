using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class DeleteUser : Form
    {
        private readonly LogService logService;
        private readonly UserService userService;

        public DeleteUser()
        {
            InitializeComponent();

            logService = new LogService(new LogRepository());
            userService = new UserService(new UserRepository(), logService);
        }

        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            string userIdText = txtUserID.Text.Trim();

            if (!int.TryParse(userIdText, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            try
            {
                var userToDelete = new User { ID = userId };
                userService.DeleteUser(userToDelete);

                MessageBox.Show("User deleted successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}");
            }
        }

        private void DeleteUser_Load(object sender, EventArgs e)
        {
        }
    }
}
