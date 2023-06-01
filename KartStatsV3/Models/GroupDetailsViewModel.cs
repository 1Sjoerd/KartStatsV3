using KartStatsV3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class GroupDetailsViewModel
    {
        public Group Group { get; set; }
        public List<User> Members { get; set; }
    }

}
