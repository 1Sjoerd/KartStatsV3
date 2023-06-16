using KartStatsV3.Models;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IResultService
    {
        List<LapTime> GetGroupResults(int groupId, int circuitId);
        Group GetGroup(int groupId);
        Circuit GetCircuit(int circuitId);
        List<Group> GetGroupsForUser(int userId);
        List<Circuit> GetCircuitsByGroupId(int groupId);
    }
}