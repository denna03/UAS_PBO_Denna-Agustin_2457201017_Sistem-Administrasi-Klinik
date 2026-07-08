using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using ClinicAdminApp.Reports;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Controllers
{
    public class ObatController : Controller
    {
        private readonly ObatService _service;

        public ObatController(ObatService service)
        {
            _service = service;
        }

        // ===========================
        // Menampilkan Data Obat
        // ===========================
        public IActionResult Index(string? search)
        {
            List<Obat> data;

            if (string.IsNullOrWhiteSpace(search))
                data = _service.GetAll();
            else
                data = _service.Search(search);

            ViewBag.Search = search;

            return View(data);
        }

        // ===========================
        // Form Tambah Obat
        // ===========================
        public IActionResult Create()
        {
            return View();
        }

        // ===========================
        // Simpan Data Obat
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Obat obat)
        {
            if (!ModelState.IsValid)
                return View(obat);

            _service.Create(obat);

            TempData["Success"] = "Data obat berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        // ===========================
        // Form Edit
        // ===========================
        public IActionResult Edit(Guid id)
        {
            var obat = _service.GetById(id);

            if (obat == null)
                return NotFound();

            return View(obat);
        }

        // ===========================
        // Update Data
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Obat obat)
        {
            if (!ModelState.IsValid)
                return View(obat);

            _service.Update(obat);

            TempData["Success"] = "Data obat berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        // ===========================
        // Konfirmasi Hapus
        // ===========================
        public IActionResult Delete(Guid id)
        {
            var obat = _service.GetById(id);

            if (obat == null)
                return NotFound();

            return View(obat);
        }

        // ===========================
        // Hapus Data
        // ===========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data obat berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var report = new ObatReport(_service.GetAll());

            var pdf = report.GeneratePdf();

            return File(pdf,
                "application/pdf",
                "Laporan_Obat.pdf");
        }
    }
}