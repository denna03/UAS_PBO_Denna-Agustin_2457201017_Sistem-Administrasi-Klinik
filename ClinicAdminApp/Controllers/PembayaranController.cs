using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicAdminApp.Reports;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Controllers
{
    public class PembayaranController : Controller
    {
        private readonly PembayaranService _service;
        private readonly ResepService _resepService;

        public PembayaranController(
            PembayaranService service,
            ResepService resepService)
        {
            _service = service;
            _resepService = resepService;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.ResepId = new SelectList(
                _resepService.GetAll(),
                "Id",
                "NomorResep");

            ViewBag.Metode = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value="Tunai",
                    Text="Tunai"
                },

                new SelectListItem
                {
                    Value="Transfer",
                    Text="Transfer"
                },

                new SelectListItem
                {
                    Value="QRIS",
                    Text="QRIS"
                }
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid resepId, string metode)
        {
            _service.Create(resepId, metode);

            TempData["Success"] = "Pembayaran berhasil disimpan.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var data = _service.GetById(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Pembayaran berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var report = new PembayaranReport(_service.GetAll());

            var pdf = report.GeneratePdf();

            return File(pdf,
                "application/pdf",
                "Laporan_Pembayaran.pdf");
        }
    }
}