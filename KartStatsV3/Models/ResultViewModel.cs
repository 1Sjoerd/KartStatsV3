using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class ResultViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CircuitId { get; set; }
        public string CircuitName { get; set; }
        public List<LapTime> LapTimes { get; set; }
    }
}
