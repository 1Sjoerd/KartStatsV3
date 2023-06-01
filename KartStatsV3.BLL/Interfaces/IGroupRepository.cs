using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartStatsV3.Models;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAllGroups();
        Group GetGroup(int id);
        void AddGroup(Group group);
        bool UpdateGroup(Group group);
        bool DeleteGroup(int groupId);
        string GetGroupAdmin(int groupId);
        List<User> GetGroupMembers(int groupId);
        void AddGroupMember(int groupId, string userId);
        void RemoveGroupMember(int groupId, string userId);
        void RemoveMember(int userId, int groupId);
        bool AddMember(int userId, int groupId);
        List<Group> GetGroupsForUser(int userId);
        bool IsUserInGroup(int userId, int groupId);
    }
}
