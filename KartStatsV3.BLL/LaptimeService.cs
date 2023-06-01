using System.Collections.Generic;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using System;
using KartStatsV3.Models;

namespace YourNamespace.BLL.Services
{
    public class LaptimeService : ILaptimeService
    {
        private readonly ILaptimeRepository _lapTimeDAL;

        public LaptimeService(ILaptimeRepository lapTimeDAL)
        {
            _lapTimeDAL = lapTimeDAL;
        }

        public void AddLapTime(LapTime lapTime)
        {
            _lapTimeDAL.AddLapTime(lapTime);
        }

        public void UpdateLapTime(LapTime lapTime)
        {
            _lapTimeDAL.UpdateLapTime(lapTime);
        }

        public void DeleteLapTime(int userId, int circuitId, DateTime dateTime)
        {
            _lapTimeDAL.DeleteLapTime(userId, circuitId, dateTime);
        }

        public List<LapTime> GetLapTimesByCircuit(int circuitId)
        {
            return _lapTimeDAL.GetLapTimesByCircuit(circuitId);
        }

        public List<LapTime> GetLapTimesByUser(int userId)
        {
            return _lapTimeDAL.GetLapTimesByUser(userId);
        }

        public LapTime GetLapTime(int userId, int circuitId, DateTime dateTime)
        {
            return _lapTimeDAL.GetLapTime(userId, circuitId, dateTime);
        }
    }
}
