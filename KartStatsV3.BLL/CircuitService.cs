﻿using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL
{
    public class CircuitService : ICircuitService
    {
        private readonly ICircuitRepository _circuitDAL;
        private readonly IGroupRepository _groupRepository;

        public CircuitService(ICircuitRepository circuitDAL, IGroupRepository groupRepository)
        {
            _circuitDAL = circuitDAL;
            _groupRepository = groupRepository;
        }

        public List<Circuit> GetAllCircuits()
        {
            return _circuitDAL.GetAllCircuits();
        }

        public Circuit GetCircuitById(int circuitId)
        {
            return _circuitDAL.GetCircuitById(circuitId);
        }

        public void AddCircuit(Circuit circuit)
        {
            _circuitDAL.AddCircuit(circuit);
        }

        public void UpdateCircuit(Circuit circuit)
        {
            _circuitDAL.UpdateCircuit(circuit);
        }

        public void DeleteCircuit(int circuitId)
        {
            _circuitDAL.DeleteCircuit(circuitId);
        }
        public List<Circuit> GetCircuitsByGroupId(int groupId)
        {
            return _circuitDAL.GetCircuitsByGroupId(groupId);
        }
    }
}
