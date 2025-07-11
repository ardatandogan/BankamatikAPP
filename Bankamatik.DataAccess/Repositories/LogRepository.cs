using Bankamatik.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bankamatik.DataAccess.Repositories
{
    public class LogRepository
    {
        private readonly string _connectionString;

        public LogRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }

        // Tüm logları getirir
       

        // Tüm filtreleri destekleyen ortak metot (iç kullanım)
        public List<Log> GetLogsByFilters(Log log)
     {
            var logs = new List<Log>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetAllLogs", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", log.UserID );
                command.Parameters.AddWithValue("@ActionType", log.ActionType );
                command.Parameters.AddWithValue("@StartDate", log.StartDate);
                command.Parameters.AddWithValue("@EndDate", log.EndDate );

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new Log
                        {
                            LogID = Convert.ToInt32(reader["LogID"]),
                            UserID = reader["UserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UserID"]),
                            ActionType = reader["ActionType"]?.ToString() ?? string.Empty,
                            Description = reader["Description"]?.ToString() ?? string.Empty,
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        });
                    }
                }
            }

            return logs;
        }

        // Yeni log kaydı ekler
        public void InsertLog(Log log)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_InsertLog", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", log.UserID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ActionType", log.ActionType);
                command.Parameters.AddWithValue("@Description", log.Description);
                command.Parameters.AddWithValue("@CreatedAt", log.CreatedAt);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
