using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using YourNamespace.BLL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using KartStatsV3.BLL;

namespace YourNamespace.Controllers
{
    public class LapTimeController : Controller
    {
        private readonly ILaptimeService _laptimeService;
        private readonly ICircuitService _circuitService;

        public LapTimeController(ILaptimeService lapTimeBLL, ICircuitService circuitService)
        {
            _laptimeService = lapTimeBLL;
            _circuitService = circuitService;
        }

        public IActionResult Index()
        {
            List<Circuit> circuits = _circuitService.GetAllCircuits();
            Dictionary<int, string> circuitDictionary = circuits.ToDictionary(c => c.CircuitId, c => c.Name);
            ViewBag.Circuits = circuitDictionary;
            List<LapTime> lapTimes = _laptimeService.GetLapTimesByUser((int)HttpContext.Session.GetInt32("Id"));
            return View(lapTimes);
        }

        public IActionResult Create()
        {
            List<Circuit> circuits = _circuitService.GetAllCircuits();
            ViewBag.Circuits = new SelectList(circuits, "CircuitId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(LapTime lapTime)
        {
            if (ModelState.IsValid)
            {
                _laptimeService.AddLapTime(lapTime);
                return RedirectToAction("Index", "Laptime");
            }

            return View(lapTime);
        }

        public IActionResult Edit(int userId, int circuitId, DateTime dateTime)
        {
            LapTime lapTime = _laptimeService.GetLapTime(userId, circuitId, dateTime);

            if (lapTime == null)
            {
                return NotFound();
            }

            List<Circuit> circuits = _circuitService.GetAllCircuits();

            if (circuits == null || !circuits.Any())
            {
                return NotFound("Geen circuits gevonden.");
            }

            ViewBag.Circuits = new SelectList(circuits, "CircuitId", "Name");

            return View(lapTime);
        }

        [HttpPost]
        public IActionResult Edit(LapTime lapTime)
        {
            if (ModelState.IsValid)
            {
                _laptimeService.UpdateLapTime(lapTime);
                return RedirectToAction("Index", "Laptime");
            }

            List<Circuit> circuits = _circuitService.GetAllCircuits();
            ViewBag.Circuits = new SelectList(circuits, "CircuitId", "Name");

            return View(lapTime);
        }

        public IActionResult Delete(int userId, int circuitId, DateTime dateTime)
        {
            LapTime lapTime = _laptimeService.GetLapTime(userId, circuitId, dateTime);

            if (lapTime == null)
            {
                return NotFound();
            }

            return View(lapTime);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int userId, int circuitId, DateTime dateTime)
        {
            _laptimeService.DeleteLapTime(userId, circuitId, dateTime);
            return RedirectToAction("Index", "Laptime");
        }
    }
}
