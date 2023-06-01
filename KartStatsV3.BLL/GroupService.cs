using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartStatsV3.Models;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.BLL;

namespace KartStatsV3.BLL
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void AddGroup(Group group)
        {
            _groupRepository.AddGroup(group);
        }

        public List<Group> GetAllGroups()
        {
            return _groupRepository.GetAllGroups();
        }

        public Group GetGroup(int id)
        {
            return _groupRepository.GetGroup(id);
        }

        public bool UpdateGroup(Group group)
        {
            return _groupRepository.UpdateGroup(group);
        }

        public bool DeleteGroup(int groupId)
        {
            return _groupRepository.DeleteGroup(groupId);
        }

        public void RemoveMember(int userId, int groupId)
        {
            _groupRepository.RemoveMember(userId, groupId);
        }

        public bool AddMember(int groupId, int userId)
        {
            return _groupRepository.AddMember(groupId, userId);
        }

        public List<Group> GetGroupsForUser(int userId)
        {
            return _groupRepository.GetGroupsForUser(userId);
        }
        public bool IsUserInGroup(int userId, int groupId)
        {
            return _groupRepository.IsUserInGroup(userId, groupId);
        }

        public List<User> GetGroupMembers(int groupId)
        {
            return _groupRepository.GetGroupMembers(groupId);
        }
    }
}