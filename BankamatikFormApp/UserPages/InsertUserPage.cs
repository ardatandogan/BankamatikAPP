using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class InsertUserPage : Form
    {
        private readonly LogService logService = new LogService(new LogRepository());
        private readonly UserService userService;

        public InsertUserPage()
        {
            InitializeComponent();
            userService = new UserService(new UserRepository(), logService);
        }

        private void InsertUserPage_Load(object sender, EventArgs e)
        {

        }

        private void btn_InsertUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new User
            {
                Username = username,
                PasswordHash = password,
                Role = "User" // varsayılan rol
            };

            try
            {
                string resultMessage = userService.CreateUser(newUser);
                MessageBox.Show(resultMessage, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // İsteğe bağlı: oluşturulan kullanıcıyı geri al
                var createdUser = userService.GetUserByUsername(new User { Username = newUser.Username });

                // Log kaydı da burada eklenebilir (dilersen ekleyebilirim)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
