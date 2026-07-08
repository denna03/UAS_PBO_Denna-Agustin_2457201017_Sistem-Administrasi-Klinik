using ClinicAdminApp.Models;
using ClinicAdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicAdminApp.Controllers
{
    public class PendaftaranController : Controller
    {
        private readonly PendaftaranService _pendaftaranService;
        private readonly PasienService _pasienService;
        private readonly DokterService _dokterService;
        private readonly PoliService _poliService;

        public PendaftaranController(
            PendaftaranService pendaftaranService,
            PasienService pasienService,
            DokterService dokterService,
            PoliService poliService)
        {
            _pendaftaranService = pendaftaranService;
            _pasienService = pasienService;
            _dokterService = dokterService;
            _poliService = poliService;
        }

        public IActionResult Index()
        {
            return View(_pendaftaranService.GetAll());
        }

        public IActionResult Create()
        {
            LoadDropdown();

            return View(new Pendaftaran
            {
                TanggalBerobat = DateTime.Today,
                Status = "Menunggu"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pendaftaran model)
        {
            // Isi field otomatis sebelum validasi
            model.NoPendaftaran = GenerateNoPendaftaran();

            if (string.IsNullOrWhiteSpace(model.Status))
                model.Status = "Menunggu";

            // Hapus validasi lama untuk NoPendaftaran
            ModelState.Remove(nameof(Pendaftaran.NoPendaftaran));

            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine($"{item.Key} => {error.ErrorMessage}");
                    }
                }

                LoadDropdown();
                return View(model);
            }

            try
            {
                _pendaftaranService.Create(model);

                TempData["Success"] = "Pendaftaran berhasil disimpan.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);

                LoadDropdown();

                return View(model);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var data = _pendaftaranService.GetById(id);

            if (data == null)
                return NotFound();

            LoadDropdown();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pendaftaran model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown();
                return View(model);
            }

            _pendaftaranService.Update(model);

            TempData["Success"] = "Data berhasil diperbarui.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var data = _pendaftaranService.GetById(id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _pendaftaranService.Delete(id);

            TempData["Success"] = "Data berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdown()
        {
            ViewBag.PasienId = new SelectList(
                _pasienService.GetAll(),
                "Id",
                "NamaPasien");

            ViewBag.DokterId = new SelectList(
                _dokterService.GetAll(),
                "Id",
                "NamaDokter");

            ViewBag.PoliId = new SelectList(
                _poliService.GetAll(),
                "Id",
                "NamaPoli");
        }

        private string GenerateNoPendaftaran()
        {
            return "REG" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}