using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartStatsV3.Models
{
    public class Group
    {
        public int GroupId { get; private set; }
        public string Name { get; private set; }
        public int AdminUserId { get; private set; }
        public string AdminUserName { get; private set; }

        public Group(int groupId, string name, int adminUserId, string adminUserName)
        {
            GroupId = groupId;
            Name = name;
            AdminUserId = adminUserId;
            AdminUserName = adminUserName;
        }

        public Group(int groupId, string name, int adminUserId)
        {
            GroupId = groupId;
            Name = name;
            AdminUserId = adminUserId;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }


}