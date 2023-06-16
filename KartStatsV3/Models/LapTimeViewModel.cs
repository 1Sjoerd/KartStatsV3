using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class LapTimeViewModel
    {
        public int UserId { get; set; }
        public int CircuitId { get; set; }
        public DateTime DateTime { get; set; }
        public string? Time { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }
    }
}
