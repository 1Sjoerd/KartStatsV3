using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KartStatsV3.Models
{
    public class LapTimeCreateEditViewModel
    {
        public LapTime LapTime { get; set; }
        public SelectList Circuits { get; set; }
    }
}
