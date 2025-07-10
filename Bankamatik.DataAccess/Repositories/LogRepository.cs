using Bankamatik.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankamatik.DataAccess.Repositories
{
    public class LogRepository
    {
        private readonly string _connectionString;

        public LogRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }
        public List<Log> GetAllLogs()
        {
            var logs = new List<Log>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetAllLogs", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                logs.Add(new Log
                {
                    LogID = Convert.ToInt32(reader["LogID"]),
                    UserID = reader["UserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UserID"]),
                    ActionType = reader["ActionType"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }
            return logs;
        }

        public void InsertLog(Log log)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InsertLog", connection);
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
