using KartStatsV3.Models;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IResultService
    {
        List<LapTime> GetGroupResults(int groupId, int circuitId);
    }
}