using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL.Interfaces
{
    public interface ILaptimeRepository
    {
        void AddLapTime(LapTime lapTime);
        void UpdateLapTime(LapTime lapTime);
        void DeleteLapTime(int userId, int circuitId, DateTime dateTime);
        List<LapTime> GetLapTimesByCircuit(int circuitId);
        List<LapTime> GetLapTimesByUser(int userId);
        LapTime GetLapTime(int userId, int circuitId, DateTime dateTime);
    }
}
