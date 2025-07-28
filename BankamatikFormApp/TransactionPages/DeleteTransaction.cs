using Bankamatik.Business.Services;
using System;
using System.Windows.Forms;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;

namespace BankamatikFormApp
{
    public partial class DeleteTransaction : Form
    {
        private readonly LogService logService = new LogService(new LogRepository());

        private readonly TransactionService transactionService = new TransactionService(
            new TransactionRepository(),
            new AccountRepository(),
            new LogService(new LogRepository())
        );
        // Giriş yapan kullanıcıyı buraya set etmen gerekebilir
        public User? CurrentUser { get; set; }

        public DeleteTransaction()
        {
            InitializeComponent();
        }

        private void btn_DeleteTransaction_Click(object sender, EventArgs e)
        {
            string transactionIDText = txtTransactionID.Text.Trim();

            if (!int.TryParse(transactionIDText, out int transactionID))
            {
                MessageBox.Show("Please enter a valid numeric Transaction ID.");
                return;
            }

            try
            {
                transactionService.DeleteTransaction(new Transaction { TransactionID = transactionID });

                // Log ekle
                

                MessageBox.Show("Transaction deleted successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteTransaction_Load(object sender, EventArgs e)
        {
        }
    }
}
