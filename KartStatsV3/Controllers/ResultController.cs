using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;

namespace KartStatsV3.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        public IActionResult Index(int? groupId, int? circuitId)
        {
            var model = new ResultViewModel
            {
                AllGroups = _resultService.GetGroupsForUser((int)HttpContext.Session.GetInt32("Id")),
            };

            if (groupId.HasValue)
            {
                var group = _resultService.GetGroup(groupId.Value);
                model.GroupId = groupId.Value;
                model.GroupName = group.Name;
                model.GroupCircuits = _resultService.GetCircuitsByGroupId(groupId.Value); // fetch circuits
            }

            if (circuitId.HasValue)
            {
                var circuit = _resultService.GetCircuit(circuitId.Value);
                model.CircuitId = circuitId.Value;
                model.CircuitName = circuit.Name;
            }

            if (groupId.HasValue && circuitId.HasValue)
            {
                model.LapTimes = _resultService.GetGroupResults(groupId.Value, circuitId.Value);
            }

            return View(model);
        }
    }


}
