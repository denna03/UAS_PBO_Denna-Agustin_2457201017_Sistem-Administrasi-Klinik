using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAdminApp.Controllers
{
    public class PoliController : Controller
    {
        private readonly PoliService _service;

        public PoliController(PoliService service)
        {
            _service = service;
        }

        public IActionResult Index(string? search)
        {
            var data = string.IsNullOrWhiteSpace(search)
                ? _service.GetAll()
                : _service.Search(search);

            ViewBag.Search = search;

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Poli poli)
        {
            if (!ModelState.IsValid)
                return View(poli);

            _service.Create(poli);

            TempData["Success"] = "Data poli berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var poli = _service.GetById(id);

            if (poli == null)
                return NotFound();

            return View(poli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Poli poli)
        {
            if (!ModelState.IsValid)
                return View(poli);

            _service.Update(poli);

            TempData["Success"] = "Data poli berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var poli = _service.GetById(id);

            if (poli == null)
                return NotFound();

            return View(poli);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data poli berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}