using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using Microsoft.AspNetCore.Mvc;

namespace KartStatsV3.Controllers
{
    public class CircuitController : Controller
    {
        private readonly ICircuitService _circuitService;

        public CircuitController(ICircuitService circuitBLL)
        {
            _circuitService = circuitBLL;
        }

        public IActionResult Index()
        {
            var circuits = _circuitService.GetAllCircuits();
            return View(circuits);
        }

        public IActionResult Details(int id)
        {
            var circuit = _circuitService.GetCircuitById(id);

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
                _circuitService.AddCircuit(circuit);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var circuit = _circuitService.GetCircuitById(id);

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
                _circuitService.UpdateCircuit(circuit);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var circuit = _circuitService.GetCircuitById(id);

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
            _circuitService.DeleteCircuit(id);
            return RedirectToAction("Index");
        }
    }
}
