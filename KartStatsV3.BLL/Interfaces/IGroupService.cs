using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartStatsV3.Models;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IGroupService
    {
        List<Group> GetAllGroups();
        Group GetGroup(int id);
        void AddGroup(Group group);
        bool UpdateGroup(Group group);
        bool DeleteGroup(int groupId);
        bool RemoveMember(int userId, int groupId);
        bool AddMember(int userId, int groupId);
        List<Group> GetGroupsForUser(int userId);
        bool IsUserInGroup(int userId, int groupId);
        List<User> GetGroupMembers(int groupId);
        void AddCircuitToGroup(int groupId, int circuitId);
    }
}