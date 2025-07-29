using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Account> GetAccounts(Account? account) 
        {
            var accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAccounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@AccountID", account?.AccountID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", account?.UserID ?? (object)DBNull.Value);
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
                            UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserID")),
                            Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                            ParaCinsi = reader.GetString(reader.GetOrdinal("ParaCinsi")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        });
                    }
                }
            }

            return accounts;
        }

        public Account? GetAccountById(Account account)
        {
            Account? result = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetAccount @AccountID, @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);
                    if (account.UserID.HasValue)
                        cmd.Parameters.AddWithValue("@UserID", account.UserID.Value);
                    else
                        cmd.Parameters.AddWithValue("@UserID", DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Account
                            {
                                AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                                UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserID")),
                                Balance = reader.GetDecimal(reader.GetOrdinal("Balance")),
                                ParaCinsi = reader.GetString(reader.GetOrdinal("ParaCinsi")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                        }
                    }
                }
            }
            return result;
        }

        public void InsertAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_InsertAccount @UserID, @Balance, @ParaCinsi, @Created";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", account.UserID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);
                    cmd.Parameters.AddWithValue("@ParaCinsi", account.ParaCinsi ?? (object)DBNull.Value);

                    if (account.CreatedAt > DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@Created", account.CreatedAt);
                    else
                        cmd.Parameters.AddWithValue("@Created", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    
        public void UpdateAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_UpdateAccount @AccountID, @UserID, @Balance, @ParaCinsi, @CreatedAt";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", account.AccountID);
                    cmd.Parameters.AddWithValue("@UserID", (object?)account.UserID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Balance", (object?)account.Balance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ParaCinsi", account.ParaCinsi ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedAt", account.CreatedAt > DateTime.MinValue ? account.CreatedAt : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
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
    }
}