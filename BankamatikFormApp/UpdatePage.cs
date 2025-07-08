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
            CurrentUser.PasswordHash = txtPasswordHash.Text;
            MessageBox.Show(userService.UpdateUser(CurrentUser));
        }

        private void UpdatePage_Load(object sender, EventArgs e)
        {
            txtUsername.Text = CurrentUser.Username;    
            txtPasswordHash.Text = CurrentUser.PasswordHash;    
            txtID.Text = CurrentUser.ID.ToString();    
        }
    }
}
