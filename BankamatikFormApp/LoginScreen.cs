using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankamatikFormApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please write username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loginDto = new
            {
                Username = username,
                PasswordHash = password
            };

            try
            {
                UserService userService = new UserService(new Bankamatik.DataAccess.Repositories.UserRepository());
                User? x = userService.GetUserByUsername(new User { Username = textBox1.Text, PasswordHash = textBox2.Text });

                if (x != null && x.ID > 0)
                {
                    MessageBox.Show("Login Succeed.", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainPage mainPage = new MainPage();
                    mainPage.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request error occured.: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
