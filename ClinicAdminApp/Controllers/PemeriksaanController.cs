using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicAdminApp.Controllers
{
    public class PemeriksaanController : Controller
    {
        private readonly PemeriksaanService _pemeriksaanService;
        private readonly PendaftaranService _pendaftaranService;

        public PemeriksaanController(
            PemeriksaanService pemeriksaanService,
            PendaftaranService pendaftaranService)
        {
            _pemeriksaanService = pemeriksaanService;
            _pendaftaranService = pendaftaranService;
        }

        public IActionResult Index()
        {
            var data = _pemeriksaanService.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            LoadDropdown();

            return View(new Pemeriksaan());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pemeriksaan model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown();
                return View(model);
            }

            _pemeriksaanService.Create(model);

            TempData["Success"] = "Pemeriksaan berhasil disimpan.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var data = _pemeriksaanService.GetById(id);

            if (data == null)
                return NotFound();

            LoadDropdown();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pemeriksaan model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown();
                return View(model);
            }

            _pemeriksaanService.Update(model);

            TempData["Success"] = "Pemeriksaan berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var data = _pemeriksaanService.GetById(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _pemeriksaanService.Delete(id);

            TempData["Success"] = "Data berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdown()
        {
            var data = _pendaftaranService.GetAll();

            ViewBag.PendaftaranId = new SelectList(
                data,
                "Id",
                "NoPendaftaran");
        }
    }
}