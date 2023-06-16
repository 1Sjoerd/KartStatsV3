using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Web;
using KartStatsV3.Models;
using KartStatsV3.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace KartStatsV3.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public UserRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MyConnectionStringName");
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT Id, Username, PasswordHash, Email FROM Users WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),     // Id
                                reader.GetString(1),   // Username
                                reader.GetString(2),   // PasswordHash
                                reader.GetString(3)    // Email
                            );
                        }
                    }
                }
            }

            return user;
        }

        public User GetUserById(int id)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT Id, Username, PasswordHash, Email FROM Users WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),     // Id
                                reader.GetString(1),   // Username
                                reader.GetString(2),   // PasswordHash
                                reader.GetString(3)    // Email
                            );
                        }
                    }
                }
            }

            return user;
        }

        public string GetUsername()
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT Id, Username, PasswordHash, Email FROM Users WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", Session.GetString("Username"));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3)
                            );
                        }
                    }
                }
            }

            return user.Username;
        }

        public int? GetId(string username)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT Id, Username, PasswordHash, Email FROM Users WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3)
                            );
                        }
                    }
                }
            }

            return user?.Id;
        }

        public void CreateUser(User user, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)", connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", HashPassword(password));
                    command.Parameters.AddWithValue("@Email", user.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
