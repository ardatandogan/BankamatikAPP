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

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        // Stored procedure ile Account ve ilişkili Transactions silme
        public void DeleteAccountWithTransactions(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_DeleteAccountWithTransactions", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@AccountID", accountId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        // GET LIST - filtre olarak Account nesnesi alır (ör. UserID filtre için)
        public List<Account> GetAccounts(Account? filter = null)
        {
            var accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetAccounts @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", (object?)filter?.UserID ?? DBNull.Value);

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
            }

            return accounts;
        }

        // GET BY ID - sadece accountId alır (özel durum)
        public Account? GetAccountById(int accountId)
        {
            Account? account = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetAccount @AccountID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
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

            return account;
        }

        // INSERT - entity parametre alır
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

        // UPDATE - entity parametre alır, nullable değerleri DBNull olarak gönderir
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

        // DELETE - entity parametre alır
        public void DeleteAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Önce ilişkili transactionları sil
                string deleteTransactionsSql = "DELETE FROM Transactions WHERE FromAccountID = @AccountID OR ToAccountID = @AccountID";
                using (SqlCommand cmd = new SqlCommand(deleteTransactionsSql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);
                    cmd.ExecuteNonQuery();
                }

                // Sonra hesabı sil
                string deleteAccountSql = "EXEC sp_DeleteAccount @AccountID";
                using (SqlCommand cmd = new SqlCommand(deleteAccountSql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
