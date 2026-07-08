using ClinicAdminApp.Models.ViewModels;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicAdminApp.Controllers
{
    public class ResepController : Controller
    {
        private readonly ResepService _resepService;
        private readonly PemeriksaanService _pemeriksaanService;
        private readonly ObatService _obatService;

        public ResepController(
            ResepService resepService,
            PemeriksaanService pemeriksaanService,
            ObatService obatService)
        {
            _resepService = resepService;
            _pemeriksaanService = pemeriksaanService;
            _obatService = obatService;
        }

        public IActionResult Index()
        {
            var data = _resepService.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.PemeriksaanId = new SelectList(
                _pemeriksaanService.GetAll(),
                "Id",
                "Id");

            ViewBag.ObatId = new SelectList(
                _obatService.GetAll(),
                "Id",
                "NamaObat");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResepViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PemeriksaanId = new SelectList(
                    _pemeriksaanService.GetAll(),
                    "Id",
                    "Id");

                ViewBag.ObatId = new SelectList(
                    _obatService.GetAll(),
                    "Id",
                    "NamaObat");

                return View(model);
            }

            bool berhasil = _resepService.Create(model);

            if (!berhasil)
            {
                TempData["Error"] = "Stok obat tidak mencukupi.";

                return RedirectToAction(nameof(Create));
            }

            TempData["Success"] = "Resep berhasil disimpan.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var data = _resepService.GetById(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _resepService.Delete(id);

            TempData["Success"] = "Resep berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}