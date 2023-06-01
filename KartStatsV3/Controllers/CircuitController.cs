using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;

namespace KartStatsV3.Controllers
{
    public class CircuitController : Controller
    {
        private readonly ICircuitService _circuitBLL;

        public CircuitController(ICircuitService circuitBLL)
        {
            _circuitBLL = circuitBLL;
        }

        public IActionResult Index()
        {
            var circuits = _circuitBLL.GetAllCircuits();
            return View(circuits);
        }

        public IActionResult Details(int id)
        {
            var circuit = _circuitBLL.GetCircuitById(id);

            if (circuit == null)
            {
                return NotFound();
            }

            return View(circuit);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Circuit circuit)
        {
            if (ModelState.IsValid)
            {
                _circuitBLL.AddCircuit(circuit);
                return RedirectToAction("Index");
            }

            return View(circuit);
        }

        public IActionResult Edit(int id)
        {
            var circuit = _circuitBLL.GetCircuitById(id);

            if (circuit == null)
            {
                return NotFound();
            }

            return View(circuit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Circuit circuit)
        {
            if (id != circuit.CircuitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _circuitBLL.UpdateCircuit(circuit);
                return RedirectToAction("Index");
            }

            return View(circuit);
        }

        public IActionResult Delete(int id)
        {
            var circuit = _circuitBLL.GetCircuitById(id);

            if (circuit == null)
            {
                return NotFound();
            }

            return View(circuit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _circuitBLL.DeleteCircuit(id);
            return RedirectToAction("Index");
        }
    }
}
