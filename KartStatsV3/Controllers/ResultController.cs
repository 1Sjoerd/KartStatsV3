using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;

namespace KartStatsV3.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        private readonly IGroupService _groupService;
        private readonly ICircuitService _circuitService;

        public ResultController(IResultService resultService, IGroupService groupService, ICircuitService circuitService)
        {
            _resultService = resultService;
            _groupService = groupService;
            _circuitService = circuitService;
        }

        public IActionResult Index(int groupId, int circuitId)
        {
            var results = _resultService.GetGroupResults(groupId, circuitId);
            var group = _groupService.GetGroup(groupId);
            var circuit = _circuitService.GetCircuitById(circuitId);

            var model = new ResultViewModel
            {
                GroupId = groupId,
                GroupName = group.Name,
                CircuitId = circuitId,
                CircuitName = circuit.Name,
                LapTimes = results
            };

            return View(model);
        }
    }

}
