using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ClinicAdminApp.Reports;

namespace ClinicAdminApp.Controllers
{
    public class DokterController : Controller
    {
        private readonly DokterService _service;

        public DokterController(DokterService service)
        {
            _service = service;
        }

        // ==========================
        // Menampilkan Data Dokter + Search
        // ==========================
        public IActionResult Index(string? search)
        {
            List<Dokter> data;

            if (string.IsNullOrWhiteSpace(search))
            {
                data = _service.GetAll();
            }
            else
            {
                data = _service.Search(search);
            }

            ViewBag.Search = search;

            return View(data);
        }

        // ==========================
        // Form Tambah Dokter
        // ==========================
        public IActionResult Create()
        {
            return View();
        }

        // ==========================
        // Simpan Data Dokter
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dokter dokter)
        {
            if (!ModelState.IsValid)
                return View(dokter);

            _service.Create(dokter);

            TempData["Success"] = "Data dokter berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // Form Edit Dokter
        // ==========================
        public IActionResult Edit(Guid id)
        {
            var dokter = _service.GetById(id);

            if (dokter == null)
                return NotFound();

            return View(dokter);
        }

        // ==========================
        // Update Data Dokter
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dokter dokter)
        {
            if (!ModelState.IsValid)
                return View(dokter);

            _service.Update(dokter);

            TempData["Success"] = "Data dokter berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // Konfirmasi Hapus
        // ==========================
        public IActionResult Delete(Guid id)
        {
            var dokter = _service.GetById(id);

            if (dokter == null)
                return NotFound();

            return View(dokter);
        }

        // ==========================
        // Hapus Data Dokter
        // ==========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data dokter berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var data = _service.GetAll();

            var report = new DokterReport(data);

            byte[] pdf = report.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                "Laporan_Dokter.pdf");
        }
    }
}