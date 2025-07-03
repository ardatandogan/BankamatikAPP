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


        //GET LIST //parametreler dtodaki classların hepsi olacak
        public List<Account> GetAccounts(int? userId = null)
        {
            var accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_GetAccounts @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", (object)userId ?? DBNull.Value);

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

        //GET
        public Account GetAccountById(int accountId)
        {
            Account account = null;

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


        //INSERT
        public void InsertAccount(Account account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_InsertAccount @UserID, @Balance, @Created";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", account.AccountID);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);

                    if (account.CreatedAt > DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@Created", account.CreatedAt);
                    else
                        cmd.Parameters.AddWithValue("@Created", DBNull.Value);

                 //modification yapılabilir adonetteki diğer execution methoduna bak
                 cmd.ExecuteNonQuery();
                }
            }
        }
        //UPDATE
        public void UpdateAccount(int accountId, int? userId = null, decimal? balance = null, DateTime? createdAt = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "EXEC sp_UpdateAccount @AccountID, @UserID, @Balance, @CreatedAt";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);
                    cmd.Parameters.AddWithValue("@UserID", (object)userId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Balance", (object)balance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedAt", (object)createdAt ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }




        //DELETE
        public void DeleteAccount(int accountId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Önce ilişkili transactionları sil
                string deleteTransactionsSql = "DELETE FROM Transactions WHERE FromAccountID = @AccountID OR ToAccountID = @AccountID";
                using (SqlCommand cmd = new SqlCommand(deleteTransactionsSql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);
                    cmd.ExecuteNonQuery();
                }

                // Sonra hesabı sil
                string deleteAccountSql = "EXEC sp_DeleteAccount @AccountID";
                using (SqlCommand cmd = new SqlCommand(deleteAccountSql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
