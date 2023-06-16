using KartStatsV3.Models;

namespace KartStatsV3.DAL.Repositories
{
    public interface IMockResultRepository
    {
        List<LapTime> GetGroupResults(int groupId, int circuitId);
    }
}