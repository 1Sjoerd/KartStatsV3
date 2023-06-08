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
        public IActionResult Create(CircuitViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Circuit circuit = new Circuit(viewModel.CircuitId, viewModel.Name);
                _circuitBLL.AddCircuit(circuit);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var circuit = _circuitBLL.GetCircuitById(id);

            if (circuit == null)
            {
                return NotFound();
            }

            var viewModel = new CircuitViewModel
            {
                CircuitId = circuit.CircuitId,
                Name = circuit.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CircuitViewModel viewModel)
        {
            if (id != viewModel.CircuitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Circuit circuit = new Circuit(viewModel.CircuitId, viewModel.Name);
                _circuitBLL.UpdateCircuit(circuit);
                return RedirectToAction("Index");
            }

            return View(viewModel);
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
