using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL.Interfaces
{
    public interface ICircuitRepository
    {
        List<Circuit> GetAllCircuits();
        Circuit GetCircuitById(int circuitId);
        void AddCircuit(Circuit circuit);
        void UpdateCircuit(Circuit circuit);
        void DeleteCircuit(int circuitId);
        List<Circuit> GetCircuitsByGroupId(int groupId);
    }
}
