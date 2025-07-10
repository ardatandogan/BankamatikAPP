using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Bankamatik.Core.Entities;
using System;
using System.Collections.Generic;

namespace Bankamatik.DataAccess.Repositories
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }

        // GET LIST
        public List<Transaction> GetTransactions(Transaction transaction)
        {
            var transactions = new List<Transaction>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetTransactions @AccountID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {        
                    cmd.Parameters.AddWithValue("@AccountID", (object?)transaction.AccountID ?? DBNull.Value);

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

        // GET BY ID
        //parametre değişecek
        public Transaction? GetTransactionById(Transaction transaction)
        {
            Transaction? result = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetTransaction @TransactionID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Transaction
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

            return result;
        }


        // INSERT
        public void InsertTransaction(Transaction transaction)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_InsertTransaction @FromAccountID, @ToAccountID, @Amount";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FromAccountID", transaction.FromAccountID);
                    cmd.Parameters.AddWithValue("@ToAccountID", transaction.ToAccountID);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // UPDATE
        public void UpdateTransaction(Transaction transaction)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_UpdateTransaction @TransactionID, @FromAccountID, @ToAccountID, @Amount, @TransactionDate";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                    cmd.Parameters.AddWithValue("@FromAccountID", transaction.FromAccountID);
                    cmd.Parameters.AddWithValue("@ToAccountID", transaction.ToAccountID);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE
        public void DeleteTransaction(Transaction transaction)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_DeleteTransaction @TransactionID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteTransactionsByAccountId(int accountId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Transactions WHERE FromAccountID = @AccountID OR ToAccountID = @AccountID";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
