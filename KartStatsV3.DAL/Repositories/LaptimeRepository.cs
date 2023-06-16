using System.Collections.Generic;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace KartStatsV3.DAL.Repositories
{
    public class LaptimeRepository : ILaptimeRepository
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public LaptimeRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionStringName");
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddLapTime(LapTime lapTime)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO LapTime (UserId, CircuitId, DateTime, Time) VALUES (@userId, @circuitId, @dateTime, @time)";
                    cmd.Parameters.AddWithValue("@userId", Session.GetInt32("Id"));
                    cmd.Parameters.AddWithValue("@circuitId", lapTime.CircuitId);
                    cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                    string timeString = lapTime.Minutes.ToString("00") + ":" + lapTime.Seconds.ToString("00") + ":" + lapTime.Milliseconds.ToString("000");
                    cmd.Parameters.AddWithValue("@time", timeString);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLapTime(LapTime lapTime)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE LapTime SET Time = @time, CircuitId = @circuitId WHERE UserId = @userId AND DateTime = @dateTime";
                    cmd.Parameters.AddWithValue("@circuitId", lapTime.CircuitId);
                    string timeString = lapTime.Minutes.ToString("00") + ":" + lapTime.Seconds.ToString("00") + ":" + lapTime.Milliseconds.ToString("000");
                    cmd.Parameters.AddWithValue("@time", timeString);
                    cmd.Parameters.AddWithValue("@userId", lapTime.UserId);
                    cmd.Parameters.AddWithValue("@dateTime", lapTime.DateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteLapTime(int userId, int circuitId, DateTime dateTime)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM LapTime WHERE UserId = @userId AND CircuitId = @circuitId AND DateTime = @dateTime";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@circuitId", circuitId);
                    cmd.Parameters.AddWithValue("@dateTime", dateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LapTime> GetLapTimesByCircuit(int circuitId)
        {
            List<LapTime> lapTimes = new List<LapTime>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT UserId, CircuitId, DateTime, Time FROM LapTime WHERE CircuitId = @circuitId";
                    cmd.Parameters.AddWithValue("@circuitId", circuitId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LapTime lapTime = new LapTime(
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["CircuitId"]),
                                Convert.ToDateTime(reader["DateTime"]),
                                Convert.ToString(reader["Time"])
                            );

                            lapTimes.Add(lapTime);
                        }
                    }
                }
            }

            return lapTimes;
        }

        public List<LapTime> GetLapTimesByUser(int userId)
        {
            List<LapTime> lapTimes = new List<LapTime>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT UserId, CircuitId, DateTime, Time FROM LapTime WHERE UserId = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LapTime lapTime = new LapTime(
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["CircuitId"]),
                                Convert.ToDateTime(reader["DateTime"]),
                                Convert.ToString(reader["Time"])
                            );

                            lapTimes.Add(lapTime);
                        }
                    }
                }
            }

            return lapTimes;
        }

        public LapTime GetLapTime(int userId, int circuitId, DateTime dateTime)
        {
            LapTime lapTime = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT UserId, CircuitId, DateTime, Time FROM LapTime WHERE UserId = @userId AND CircuitId = @circuitId AND DateTime = @dateTime";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@circuitId", circuitId);
                    cmd.Parameters.AddWithValue("@dateTime", dateTime);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lapTime = new LapTime(
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["CircuitId"]),
                                Convert.ToDateTime(reader["DateTime"]),
                                Convert.ToString(reader["Time"])
                            );
                        }
                    }
                }
            }

            return lapTime;
        }
    }
}
