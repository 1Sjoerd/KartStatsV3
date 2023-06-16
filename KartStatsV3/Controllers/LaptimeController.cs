using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using KartStatsV3.BLL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using KartStatsV3.BLL;
using KartStatsV3.DAL.Repositories;

namespace KartStatsV3.Controllers
{
    public class LapTimeController : Controller
    {
        private readonly ILaptimeService _laptimeService;

        public LapTimeController(ILaptimeService lapTimeBLL)
        {
            _laptimeService = lapTimeBLL;
        }

        public IActionResult Index()
        {
            Dictionary<int, string> circuitDictionary = _laptimeService.GetCircuitsDictionary();
            ViewBag.Circuits = circuitDictionary;
            List<LapTime> lapTimes = _laptimeService.GetLapTimesByUser((int)HttpContext.Session.GetInt32("Id"));
            return View(lapTimes);
        }

        public IActionResult Create()
        {
            List<Circuit> circuits = _laptimeService.GetCircuitsSelectList();
            ViewBag.Circuits = new SelectList(circuits, "CircuitId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LapTimeViewModel lapTimeViewModel)
        {
            if (ModelState.IsValid)
            {
                var lapTime = new LapTime(
                    lapTimeViewModel.UserId,
                    lapTimeViewModel.CircuitId,
                    lapTimeViewModel.DateTime,
                    lapTimeViewModel.Time,
                    lapTimeViewModel.Minutes,
                    lapTimeViewModel.Seconds,
                    lapTimeViewModel.Milliseconds);

                _laptimeService.AddLapTime(lapTime);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Circuits = _laptimeService.GetCircuitsSelectList();
            return View(lapTimeViewModel);
        }

        public IActionResult Edit(int userId, int circuitId, DateTime dateTime)
        {
            LapTime lapTime = _laptimeService.GetLapTime(userId, circuitId, dateTime);

            if (lapTime == null)
            {
                return NotFound();
            }

            List<Circuit> circuits = _laptimeService.GetCircuitsSelectList();
            ViewBag.Circuits = new SelectList(circuits, "CircuitId", "Name");
            return View(lapTime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LapTimeViewModel lapTimeViewModel)
        {
            if (ModelState.IsValid)
            {
                LapTime lapTime = new LapTime(
                    lapTimeViewModel.UserId,
                    lapTimeViewModel.CircuitId,
                    lapTimeViewModel.DateTime,
                    lapTimeViewModel.Time,
                    lapTimeViewModel.Minutes,
                    lapTimeViewModel.Seconds,
                    lapTimeViewModel.Milliseconds
                );

                _laptimeService.UpdateLapTime(lapTime);
                return RedirectToAction("Index", "Laptime");
            }

            ViewBag.Circuits = _laptimeService.GetCircuitsSelectList();
            return View(lapTimeViewModel);
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
