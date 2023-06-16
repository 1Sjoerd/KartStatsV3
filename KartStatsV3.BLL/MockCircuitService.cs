using KartStatsV3.BLL.Interfaces;
using KartStatsV3.DAL.Repositories;
using KartStatsV3.Models;

public class MockCircuitService : ICircuitService
{
    public Circuit GetCircuitById(int circuitId)
    {
        if (circuitId > 0)
        {
            return new Circuit(circuitId, "TestCircuit");
        }
        else
        {
            throw new ArgumentNullException("Circuit niet gevonden");
        }
    }

    void ICircuitService.AddCircuit(Circuit circuit)
    {
        throw new NotImplementedException();
    }

    void ICircuitService.DeleteCircuit(int circuitId)
    {
        throw new NotImplementedException();
    }

    List<Circuit> ICircuitService.GetAllCircuits()
    {
        throw new NotImplementedException();
    }

    List<Circuit> ICircuitService.GetCircuitsByGroupId(int groupId)
    {
        throw new NotImplementedException();
    }

    void ICircuitService.UpdateCircuit(Circuit circuit)
    {
        throw new NotImplementedException();
    }
}

