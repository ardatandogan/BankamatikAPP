using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Bankamatik.Core.Entities;

namespace Bankamatik.DataAccess.Repositories
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }

        public int DeleteAccount(Account account)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DELETE FROM Accounts WHERE AccountID = @AccountID", connection);
            command.Parameters.AddWithValue("@AccountID", account.AccountID);

            connection.Open();
            return command.ExecuteNonQuery();
        }
        public void DeleteTransactionsByAccountId(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = "DELETE FROM Transactions WHERE FromAccountID = @AccountID OR ToAccountID = @AccountID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@AccountID", accountId);

            connection.Open();
            command.ExecuteNonQuery();
        }
        public void DeleteAccountWithTransactions(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                // Önce transactions sil
                var deleteTransactionsCmd = new SqlCommand("DELETE FROM Transactions WHERE FromAccountID = @AccountID OR ToAccountID = @AccountID", connection, transaction);
                deleteTransactionsCmd.Parameters.AddWithValue("@AccountID", accountId);
                deleteTransactionsCmd.ExecuteNonQuery();

                // Sonra account sil
                var deleteAccountCmd = new SqlCommand("DELETE FROM Accounts WHERE AccountID = @AccountID", connection, transaction);
                deleteAccountCmd.Parameters.AddWithValue("@AccountID", accountId);
                deleteAccountCmd.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // GET LIST 
        public List<Account> GetAccounts(Account account)
        {
            var accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAccounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Account nesnesinden gelen filtreleri kontrol et
                cmd.Parameters.AddWithValue("@UserID", (object?)account.UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BalanceMin", DBNull.Value);
                cmd.Parameters.AddWithValue("@BalanceMax", DBNull.Value);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        });
                    }
                }
            }

            return accounts;
        }


        // GET BY ID 
        public Account? GetAccountById(Account account)
        {
            Account? result = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetAccount @AccountID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Account
                            {
                                AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                        }
                    }
                }
            }
            return result;
        }


        // INSERT 
        public void InsertAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_InsertAccount @UserID, @Balance, @Created";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", account.UserID);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);

                    if (account.CreatedAt > DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@Created", account.CreatedAt);
                    else
                        cmd.Parameters.AddWithValue("@Created", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // UPDATE 
        public void UpdateAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_UpdateAccount @AccountID, @UserID, @Balance, @CreatedAt";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);
                    cmd.Parameters.AddWithValue("@UserID", (object?)account.UserID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Balance", (object?)account.Balance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedAt", account.CreatedAt > DateTime.MinValue ? account.CreatedAt : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
