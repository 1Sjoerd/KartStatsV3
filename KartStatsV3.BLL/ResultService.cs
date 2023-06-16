using KartStatsV3.BLL.Interfaces;
using KartStatsV3.DAL.Repositories;
using KartStatsV3.Models;

public class ResultService : IResultService
{
    private readonly IResultRepository _resultRepository;
    private readonly IGroupService _groupService;
    private readonly ICircuitService _circuitService;

    public ResultService(IResultRepository resultRepository, IGroupService groupService, ICircuitService circuitService)
    {
        _resultRepository = resultRepository;
        _groupService = groupService;
        _circuitService = circuitService;
    }

    public List<LapTime> GetGroupResults(int groupId, int circuitId)
    {
        var group = _groupService.GetGroup(groupId);
        if (group == null)
        {
            throw new ArgumentNullException("Groep niet gevonden");
        }
        var circuit = _circuitService.GetCircuitById(circuitId);
        if (circuit == null)
        {
            throw new ArgumentNullException("Circuit niet gevonden");
        }
        return _resultRepository.GetGroupResults(groupId, circuitId);
    }

    public Group GetGroup(int groupId)
    {
        return _groupService.GetGroup(groupId);
    }

    public Circuit GetCircuit(int circuitId)
    {
        return _circuitService.GetCircuitById(circuitId);
    }

    public List<Group> GetGroupsForUser(int userId)
    {
        return _groupService.GetGroupsForUser(userId);
    }

    public List<Circuit> GetCircuitsByGroupId(int groupId)
    {
        return _circuitService.GetCircuitsByGroupId(groupId);
    }
}
