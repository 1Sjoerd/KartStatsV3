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
    public class MockResultRepository : IResultRepository
    {
        public List<LapTime> GetGroupResults(int groupId, int circuitId)
        {
            if (groupId > 0 && circuitId > 0)
            {
                return new List<LapTime>()
                {
                    new LapTime(1, circuitId, DateTime.Now, "00:02:15", 2, 15, 0),
                    new LapTime(2, circuitId, DateTime.Now, "00:02:10", 2, 10, 0)
                };
            }
            else
            {
                return null;
            }
        }
    }
}