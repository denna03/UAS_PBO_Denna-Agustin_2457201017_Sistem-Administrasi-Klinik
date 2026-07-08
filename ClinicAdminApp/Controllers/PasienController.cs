using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAdminApp.Controllers
{
    public class PasienController : Controller
    {
        private readonly PasienService _service;

        public PasienController(PasienService service)
        {
            _service = service;
        }

        public IActionResult Index(string? search)
        {
            List<Pasien> data;

            if (string.IsNullOrWhiteSpace(search))
                data = _service.GetAll();
            else
                data = _service.Search(search);

            ViewBag.Search = search;

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pasien pasien)
        {
            if (!ModelState.IsValid)
                return View(pasien);

            _service.Create(pasien);

            TempData["Success"] = "Data pasien berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var pasien = _service.GetById(id);

            if (pasien == null)
                return NotFound();

            return View(pasien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pasien pasien)
        {
            if (!ModelState.IsValid)
                return View(pasien);

            _service.Update(pasien);

            TempData["Success"] = "Data pasien berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var pasien = _service.GetById(id);

            if (pasien == null)
                return NotFound();

            return View(pasien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data pasien berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}