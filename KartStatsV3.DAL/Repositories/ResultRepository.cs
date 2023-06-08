using KartStatsV3.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.DAL.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly string _connectionString;

        public ResultRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionStringName");
        }

        public List<LapTime> GetGroupResults(int groupId, int circuitId)
        {
            List<LapTime> lapTimes = new List<LapTime>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                SELECT l.UserId, l.CircuitId, l.DateTime, l.Time
                FROM (
                    SELECT *,
                           (
                               SELECT COUNT(*)
                               FROM (
                                   SELECT l.* FROM LapTime l 
                                   INNER JOIN GroupMembers g on l.UserId = g.UserId 
                                   WHERE g.GroupId = @GroupId AND l.CircuitId = @CircuitId 
                                   UNION 
                                   SELECT l.* FROM LapTime l 
                                   INNER JOIN Groups g on l.UserId = g.AdminUserId 
                                   WHERE g.GroupId = @GroupId AND l.CircuitId = @CircuitId
                               ) AS l2
                               WHERE l2.UserId = l.UserId AND l2.DateTime >= l.DateTime
                           ) AS rank
                    FROM (
                        SELECT l.* FROM LapTime l 
                        INNER JOIN GroupMembers g on l.UserId = g.UserId 
                        WHERE g.GroupId = @GroupId AND l.CircuitId = @CircuitId 
                        UNION 
                        SELECT l.* FROM LapTime l 
                        INNER JOIN Groups g on l.UserId = g.AdminUserId 
                        WHERE g.GroupId = @GroupId AND l.CircuitId = @CircuitId
                    ) AS l
                ) AS l
                WHERE l.rank <= 3
                ORDER BY l.DateTime DESC;";
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@CircuitId", circuitId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LapTime lapTime = new LapTime
                            {
                                UserId = reader.GetInt32("UserId"),
                                CircuitId = reader.GetInt32("CircuitId"),
                                DateTime = reader.GetDateTime("DateTime"),
                                Time = reader.GetString("Time")
                            };
                            lapTimes.Add(lapTime);
                        }
                    }
                }
            }

            return lapTimes;
        }

    }
}
