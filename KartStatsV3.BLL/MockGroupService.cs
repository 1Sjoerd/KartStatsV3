using KartStatsV3.BLL.Interfaces;
using KartStatsV3.DAL.Repositories;
using KartStatsV3.Models;

public class MockGroupService : IGroupService
{
    public void AddCircuitToGroup(int groupId, int circuitId)
    {
        throw new NotImplementedException();
    }

    public void AddGroup(Group group)
    {
        throw new NotImplementedException();
    }

    public bool AddMember(int userId, int groupId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteGroup(int groupId)
    {
        throw new NotImplementedException();
    }

    public List<Group> GetAllGroups()
    {
        throw new NotImplementedException();
    }

    public Group GetGroup(int groupId)
    {
        if (groupId > 0)
        {
            return new Group(groupId, "TestGroep", 1, "TestAdminUser");
        }
        else
        {
            throw new ArgumentNullException("Groep niet gevonden");
        }
    }

    public List<User> GetGroupMembers(int groupId)
    {
        throw new NotImplementedException();
    }

    public List<Group> GetGroupsForUser(int userId)
    {
        throw new NotImplementedException();
    }

    public bool IsUserInGroup(int userId, int groupId)
    {
        throw new NotImplementedException();
    }

    public bool RemoveMember(int userId, int groupId)
    {
        throw new NotImplementedException();
    }

    public bool UpdateGroup(Group group)
    {
        throw new NotImplementedException();
    }
}


