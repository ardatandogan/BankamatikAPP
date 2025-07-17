using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Bankamatik.Core.Entities;

namespace Bankamatik.DataAccess.Repositories
{
    public class LoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }

        public List<Loan> GetLoans(Loan loan)
        {
            var loans = new List<Loan>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetLoans", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoanID", (object?)loan.LoanID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", (object?)loan.UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object?)loan.Status ?? DBNull.Value);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanID = reader.GetInt32(reader.GetOrdinal("LoanID")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            InterestRate = reader.GetDecimal(reader.GetOrdinal("InterestRate")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        });
                    }
                }
            }

            return loans;
        }

        public bool InsertLoan(Loan loan)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_InsertLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", loan.UserID);
                cmd.Parameters.AddWithValue("@Amount", loan.Amount);
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@StartDate", loan.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", loan.EndDate);
                cmd.Parameters.AddWithValue("@Status", loan.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateLoan(Loan loan)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoanID", loan.LoanID);
                cmd.Parameters.AddWithValue("@Amount", loan.Amount);
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@StartDate", loan.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", loan.EndDate);
                cmd.Parameters.AddWithValue("@Status", loan.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteLoan(int loanId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_DeleteLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoanID", loanId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }



        public bool UpdateLoanStatus(int loanId, string status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateLoanStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoanID", loanId);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

    }
}
