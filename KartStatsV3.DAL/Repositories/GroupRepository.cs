using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartStatsV3.Models;
using System.Web;
using KartStatsV3.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace KartStatsV3.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public GroupRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionStringName");
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddGroup(Group group)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Groups(Name, AdminUserId, AdminUserName) VALUES(@Name, @AdminUserId, @AdminUserName)";
                    cmd.Parameters.AddWithValue("@Name", group.Name);
                    cmd.Parameters.AddWithValue("@AdminUserId", Session.GetInt32("Id"));
                    cmd.Parameters.AddWithValue("@AdminUserName", Session.GetString("Username"));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Group> GetAllGroups()
        {
            List<Group> groups = new List<Group>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Groups.*, Users.Username as AdminUserName FROM Groups INNER JOIN Users ON Groups.AdminUserId = Users.Id";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Group group = new Group(
                                Convert.ToInt32(reader["GroupId"]),
                                reader["Name"].ToString(),
                                Convert.ToInt32(reader["AdminUserId"]),
                                reader["AdminUserName"].ToString()
                            );

                            groups.Add(group);
                        }
                    }
                }
            }

            return groups;
        }


        public Group GetGroup(int id)
        {
            Group group = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Groups.*, Users.Username as AdminUserName FROM Groups INNER JOIN Users ON Groups.AdminUserId = Users.Id WHERE Groups.GroupId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            group = new Group(
                                Convert.ToInt32(reader["GroupId"]),
                                reader["Name"].ToString(),
                                Convert.ToInt32(reader["AdminUserId"]),
                                reader["AdminUserName"].ToString()
                            );
                        }
                    }
                }
            }

            return group;
        }

        public bool UpdateGroup(Group group)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Groups SET Name = @Name, AdminUserId = @AdminUserId WHERE GroupId = @GroupId", conn);
                    cmd.Parameters.AddWithValue("@Name", group.Name);
                    cmd.Parameters.AddWithValue("@AdminUserId", Session.GetInt32("Id"));
                    cmd.Parameters.AddWithValue("@GroupId", group.GroupId);

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                // Log de fout
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Groups WHERE GroupId = @GroupId", conn);
                    cmd.Parameters.AddWithValue("@GroupId", groupId);

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                // Log de fout
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string GetGroupAdmin(int groupId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT AdminUserId FROM Groups WHERE GroupId = @GroupId";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);

                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        public List<User> GetGroupMembers(int groupId)
        {
            List<User> users = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Users.Id, Users.Username, Users.Email FROM Users INNER JOIN GroupMembers ON Users.Id = GroupMembers.UserId WHERE GroupMembers.GroupId = @groupId";
                    cmd.Parameters.AddWithValue("@groupId", groupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        public void AddGroupMember(int groupId, string userId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO GroupMembers (GroupId, UserId) VALUES (@GroupId, @UserId)";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveGroupMember(int groupId, string userId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM GroupMembers WHERE GroupId = @GroupId AND UserId = @UserId";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveMember(int userId, int groupId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM GroupMembers WHERE UserId = @UserId AND GroupId = @GroupId";
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@GroupId", groupId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool AddMember(int groupId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO GroupMembers(GroupId, UserId) VALUES(@groupId, @userId)";
                    cmd.Parameters.AddWithValue("@groupId", groupId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Group> GetGroupsForUser(int userId)
        {
            List<Group> groups = new List<Group>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT DISTINCT g.GroupId, g.Name, g.AdminUserId FROM Groups g LEFT JOIN GroupMembers gm ON g.GroupId = gm.GroupId WHERE g.AdminUserId = @userId OR gm.UserId = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Group group = new Group(
                                Convert.ToInt32(reader["GroupId"]),
                                reader["Name"].ToString(),
                                Convert.ToInt32(reader["AdminUserId"])
                            );

                            groups.Add(group);
                        }
                    }
                }
            }

            return groups;
        }

        public bool IsUserInGroup(int userId, int groupId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM GroupMembers WHERE GroupId = @groupId AND UserId = @userId";
                    cmd.Parameters.AddWithValue("@groupId", groupId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    var count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public List<Circuit> GetGroupCircuits(int groupId)
        {
            List<Circuit> circuits = new List<Circuit>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Circuits.* FROM Circuits INNER JOIN GroupCircuits ON Circuits.CircuitId = GroupCircuits.CircuitId WHERE GroupCircuits.GroupId = @GroupId";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Circuit circuit = new Circuit(
                                Convert.ToInt32(reader["CircuitId"]),
                                reader["Name"].ToString()
                            );

                            circuits.Add(circuit);
                        }
                    }
                }
            }

            return circuits;
        }

        public void AddCircuitToGroup(int groupId, int circuitId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO GroupCircuits (GroupId, CircuitId) VALUES (@GroupId, @CircuitId)";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@CircuitId", circuitId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
