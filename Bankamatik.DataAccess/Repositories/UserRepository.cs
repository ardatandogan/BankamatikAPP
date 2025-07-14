using System.Data;
using Microsoft.Data.SqlClient;
using Bankamatik.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Bankamatik.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";
        }

        // GET LIST 
        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(cmdText: "sp_GetUsers", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Username = reader["Username"].ToString() ?? "",
                            PasswordHash = reader["PasswordHash"].ToString() ?? "",
                            Role = reader["Role"].ToString() ?? "User"  
                        });
                    }
                }
            }

            return users;
        }

        // GET - Kullanıcıyı username ile getir
        public User? GetUser(User user)
        {
            User? result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new User
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Username = reader["Username"].ToString() ?? "",
                            PasswordHash = reader["PasswordHash"].ToString() ?? "",
                            Role = reader["Role"].ToString() ?? "User"
                        };
                    }
                }
            }

            return result;
        }

        // INSERT 
        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_InsertUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role ?? "User");

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // UPDATE 
        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_UpdateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", user.ID);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role ?? "User");

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // DELETE 
        public void DeleteUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_DeleteUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", user.ID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
