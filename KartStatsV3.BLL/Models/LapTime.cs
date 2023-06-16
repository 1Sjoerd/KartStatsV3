using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class LapTime
    {
        public int UserId { get; private set; }
        public int CircuitId { get; private set; }
        public DateTime DateTime { get; private set; }
        public string? Time { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int Milliseconds { get; private set; }

        public LapTime(int userId, int circuitId, DateTime dateTime, string? time, int minutes, int seconds, int milliseconds)
        {
            UserId = userId;
            CircuitId = circuitId;
            DateTime = dateTime;
            Time = time;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public LapTime(int userId, int circuitId, DateTime dateTime, string? time)
        {
            UserId = userId;
            CircuitId = circuitId;
            DateTime = dateTime;
            Time = time;
        }
    }
}
