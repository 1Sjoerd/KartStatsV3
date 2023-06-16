using System.Collections.Generic;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using System;
using KartStatsV3.Models;
using System.Web.Mvc;

namespace KartStatsV3.BLL.Services
{
    public class LaptimeService : ILaptimeService
    {
        private readonly ILaptimeRepository _lapTimeService;
        private readonly ICircuitService _circuitService;

        public LaptimeService(ILaptimeRepository lapTimeService, ICircuitService circuitService)
        {
            _lapTimeService = lapTimeService;
            _circuitService = circuitService;
        }

        public void AddLapTime(LapTime lapTime)
        {
            _lapTimeService.AddLapTime(lapTime);
        }

        public void UpdateLapTime(LapTime lapTime)
        {
            _lapTimeService.UpdateLapTime(lapTime);
        }

        public void DeleteLapTime(int userId, int circuitId, DateTime dateTime)
        {
            _lapTimeService.DeleteLapTime(userId, circuitId, dateTime);
        }

        public List<LapTime> GetLapTimesByCircuit(int circuitId)
        {
            return _lapTimeService.GetLapTimesByCircuit(circuitId);
        }

        public List<LapTime> GetLapTimesByUser(int userId)
        {
            return _lapTimeService.GetLapTimesByUser(userId);
        }

        public LapTime GetLapTime(int userId, int circuitId, DateTime dateTime)
        {
            return _lapTimeService.GetLapTime(userId, circuitId, dateTime);
        }

        public Dictionary<int, string> GetCircuitsDictionary()
        {
            List<Circuit> circuits = _circuitService.GetAllCircuits();
            return circuits.ToDictionary(c => c.CircuitId, c => c.Name);
        }

        public List<Circuit> GetCircuitsSelectList()
        {
            List<Circuit> circuits = _circuitService.GetAllCircuits();
            return circuits;
        }
    }
}
