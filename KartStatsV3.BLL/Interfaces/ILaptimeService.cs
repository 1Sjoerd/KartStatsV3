using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KartStatsV3.BLL.Interfaces
{
    public interface ILaptimeService
    {
        void AddLapTime(LapTime lapTime);
        void UpdateLapTime(LapTime lapTime);
        void DeleteLapTime(int userId, int circuitId, DateTime dateTime);
        List<LapTime> GetLapTimesByCircuit(int circuitId);
        List<LapTime> GetLapTimesByUser(int userId);
        LapTime GetLapTime(int userId, int circuitId, DateTime dateTime);
        Dictionary<int, string> GetCircuitsDictionary();
        List<Circuit> GetCircuitsSelectList();
    }
}
