using System.Data;
using Microsoft.Data.SqlClient;
using Bankamatik.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Bankamatik.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET LIST
        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetUsers", connection))
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
                            PasswordHash = reader["PasswordHash"].ToString() ?? ""
                        });
                    }
                }
            }

            return users;
        }

        // GET
        public User? GetUser(string username)
        {
            User? user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Username = reader["Username"].ToString() ?? "",
                            PasswordHash = reader["PasswordHash"].ToString() ?? ""
                        };
                    }
                }
            }

            return user;
        }

        // INSERT
        public void InsertUser(string username, string passwordHash)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_InsertUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // UPDATE
        public void UpdateUser(int id, string username, string passwordHash)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_UpdateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // DELETE
        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("sp_DeleteUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
