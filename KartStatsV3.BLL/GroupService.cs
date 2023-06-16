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
        private readonly ICircuitRepository _circuitRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupRepository groupRepository, ICircuitRepository circuitRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _circuitRepository = circuitRepository;
            _userRepository = userRepository;
        }

        public void AddGroup(Group group)
        {
            if(group == null)
            {
                throw new ArgumentNullException("Groep werd niet correct aangemaakt.");
            }

            _groupRepository.AddGroup(group);
        }

        public List<Group> GetAllGroups()
        {
            var groups = _groupRepository.GetAllGroups();

            if (groups == null || groups.Count == 0)
            {
                throw new InvalidOperationException("Er zijn geen groepen gevonden.");
            }

            return groups;
        }

        public Group GetGroup(int id)
        {
            var group = _groupRepository.GetGroup(id);
            if (group == null)
            {
                throw new ArgumentNullException("Groep niet gevonden");
            }

            return _groupRepository.GetGroup(id);
        }

        public bool UpdateGroup(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("Groep niet gevonden");
            }

            return _groupRepository.UpdateGroup(group);
        }

        public bool DeleteGroup(int groupId)
        {
            var group = _groupRepository.GetGroup(groupId);

            if (group == null)
            {
                throw new ArgumentNullException("Groep niet gevonden");
            }

            return _groupRepository.DeleteGroup(groupId);
        }

        public bool RemoveMember(int userId, int groupId)
        {
            var group = _groupRepository.GetGroup(groupId);

            if (group == null)
            {
                return false;
            }

            if (group.AdminUserId == userId)
            {
                throw new InvalidOperationException("Beheerders kunnen hun eigen groep niet verlaten");
            }

            _groupRepository.RemoveMember(userId, groupId);

            return true;
        }

        public bool AddMember(int groupId, int userId)
        {
            var group = _groupRepository.GetGroup(groupId);

            if (group == null)
            {
                throw new ArgumentNullException("Groep bestaat niet.");
            }

            if (userId < 0)
            {
                throw new ArgumentException("Gebruiker bestaat niet.");
            }

            return _groupRepository.AddMember(groupId, userId);
        }

        public List<Group> GetGroupsForUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new ArgumentException("User bestaat niet of kan niet worden gevonden");
            }

            return _groupRepository.GetGroupsForUser(userId);
        }
        public bool IsUserInGroup(int userId, int groupId)
        {
            if (!_groupRepository.IsUserInGroup(userId, groupId))
            {
                throw new ArgumentException("User zit niet in de groep");
            }

            return _groupRepository.IsUserInGroup(userId, groupId);
        }

        public List<User> GetGroupMembers(int groupId)
        {
            var groupMembers = _groupRepository.GetGroupMembers(groupId);

            if(groupMembers == null)
            {
                throw new ArgumentNullException("Geen leden gevonden");
            }

            return _groupRepository.GetGroupMembers(groupId);
        }

        public void AddCircuitToGroup(int groupId, int circuitId)
        {
            if (_groupRepository.GetGroup(groupId) == null)
            {
                throw new ArgumentNullException("Groep niet gevonden");
            }

            if (_circuitRepository.GetCircuitById(circuitId) == null)
            {
                throw new ArgumentNullException("Circuit niet gevonden");
            }

            _groupRepository.AddCircuitToGroup(groupId, circuitId);
        }
    }
}