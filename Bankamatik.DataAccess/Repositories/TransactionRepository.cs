using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Bankamatik.Core.Entities;

namespace Bankamatik.DataAccess.Repositories
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        //GET LIST
        public List<Transaction> GetTransactions(int? accountId = null)
        {
            var transactions = new List<Transaction>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetTransactionList @AccountID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", (object)accountId ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new Transaction
                            {
                                TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                                FromAccountID = reader.GetInt32(reader.GetOrdinal("FromAccountID")),
                                ToAccountID = reader.GetInt32(reader.GetOrdinal("ToAccountID")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
                            });
                        }
                    }
                }
            }

            return transactions;
        }
        //GET 
        public Transaction GetTransactionById(int transactionId)
        {
            Transaction transaction = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetTransaction @TransactionID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transaction = new Transaction
                            {
                                TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                                FromAccountID = reader.GetInt32(reader.GetOrdinal("FromAccountID")),
                                ToAccountID = reader.GetInt32(reader.GetOrdinal("ToAccountID")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
                            };
                        }
                    }
                }
            }

            return transaction;
        }
        //INSERT
        public void InsertTransaction(int fromAccountId, int toAccountId, decimal amount)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_InsertTransaction @FromAccountID, @ToAccountID, @Amount";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FromAccountID", fromAccountId);
                    cmd.Parameters.AddWithValue("@ToAccountID", toAccountId);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //DELETE
        public void DeleteTransaction(int transactionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_DeleteTransaction @TransactionID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // UPDATE
        public void UpdateTransaction(int transactionId, int fromAccountId, int toAccountId, decimal amount, DateTime transactionDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_UpdateTransaction @TransactionID, @FromAccountID, @ToAccountID, @Amount, @TransactionDate";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    cmd.Parameters.AddWithValue("@FromAccountID", fromAccountId);
                    cmd.Parameters.AddWithValue("@ToAccountID", toAccountId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
