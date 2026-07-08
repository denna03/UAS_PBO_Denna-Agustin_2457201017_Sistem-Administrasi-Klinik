using ClinicAdminApp.Data;
using ClinicAdminApp.Models;
using ClinicAdminApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ClinicAdminApp.Services
{
    public class ResepService
    {
        private readonly ApplicationDbContext _context;

        public ResepService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Resep> GetAll()
        {
            return _context.Reseps
                .Include(r => r.Pemeriksaan)
                .ThenInclude(p => p.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .OrderByDescending(r => r.TanggalResep)
                .ToList();
        }

        public Resep? GetById(Guid id)
        {
            return _context.Reseps
                .Include(r => r.Pemeriksaan)
                .ThenInclude(p => p.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .Include(r => r.DetailReseps)!
                .ThenInclude(d => d.Obat)
                .FirstOrDefault(r => r.Id == id);
        }

        public bool Create(ResepViewModel model)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var obat = _context.Obats.Find(model.ObatId);

                if (obat == null)
                    return false;

                if (obat.Stok < model.Jumlah)
                    return false;

                var resep = new Resep
                {
                    Id = Guid.NewGuid(),
                    NomorResep = GenerateNomorResep(),
                    PemeriksaanId = model.PemeriksaanId,
                    TanggalResep = DateTime.Now,
                    TotalBiaya = obat.Harga * model.Jumlah
                };

                _context.Reseps.Add(resep);

                var detail = new DetailResep
                {
                    Id = Guid.NewGuid(),
                    ResepId = resep.Id,
                    ObatId = obat.Id,
                    Jumlah = model.Jumlah,
                    Harga = obat.Harga,
                    SubTotal = obat.Harga * model.Jumlah
                };

                _context.DetailReseps.Add(detail);

                obat.Stok -= model.Jumlah;

                _context.SaveChanges();

                transaction.Commit();

                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public void Delete(Guid id)
        {
            var resep = _context.Reseps
                .Include(r => r.DetailReseps)
                .FirstOrDefault(r => r.Id == id);

            if (resep == null)
                return;

            foreach (var item in resep.DetailReseps!)
            {
                var obat = _context.Obats.Find(item.ObatId);

                if (obat != null)
                    obat.Stok += item.Jumlah;
            }

            _context.Reseps.Remove(resep);

            _context.SaveChanges();
        }

        private string GenerateNomorResep()
        {
            return "RSP" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}