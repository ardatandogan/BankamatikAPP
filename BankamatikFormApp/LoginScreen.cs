using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using System;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var userService = new UserService(new Bankamatik.DataAccess.Repositories.UserRepository());
                User? user = userService.GetUserByUsername(new User { Username = username });

                if (user == null || user.PasswordHash != password)
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var logService = new LogService(new Bankamatik.DataAccess.Repositories.LogRepository());
                logService.InsertLog(user.ID, "Login", $"{user.Username} logged in.");
                MessageBox.Show("Login successful.", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainPage mainPage = new MainPage
                {
                    CurrentUser = user  // Kullanýcýyý CurrentUser olarak atýyoruz
                };

                this.Hide(); // Login formu gizle
                mainPage.ShowDialog();
                this.Show(); // MainPage kapandýðýnda tekrar login formu göster


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
