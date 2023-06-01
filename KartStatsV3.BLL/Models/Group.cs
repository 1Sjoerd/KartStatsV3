using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartStatsV3.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int AdminUserId { get; set; }
        public string AdminUserName { get; set; }
    }

}