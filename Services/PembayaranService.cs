using ClinicAdminApp.Data;
using ClinicAdminApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAdminApp.Services
{
    public class PembayaranService
    {
        private readonly ApplicationDbContext _context;

        public PembayaranService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Pembayaran> GetAll()
        {
            return _context.Pembayarans
                .Include(x => x.Resep)
                .ThenInclude(x => x.Pemeriksaan)
                .ThenInclude(x => x.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .OrderByDescending(x => x.TanggalPembayaran)
                .ToList();
        }

        public Pembayaran? GetById(Guid id)
        {
            return _context.Pembayarans
                .Include(x => x.Resep)
                .ThenInclude(x => x.Pemeriksaan)
                .ThenInclude(x => x.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Create(Guid resepId, string metode)
        {
            var resep = _context.Reseps.Find(resepId);

            if (resep == null)
                return;

            decimal biayaPemeriksaan = 100000;

            Pembayaran pembayaran = new Pembayaran
            {
                Id = Guid.NewGuid(),
                NoPembayaran = GenerateNomor(),
                ResepId = resepId,
                BiayaPemeriksaan = biayaPemeriksaan,
                BiayaObat = resep.TotalBiaya,
                TotalBayar = biayaPemeriksaan + resep.TotalBiaya,
                MetodePembayaran = metode,
                Status = "Lunas",
                TanggalPembayaran = DateTime.Now
            };

            _context.Pembayarans.Add(pembayaran);

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var data = _context.Pembayarans.Find(id);

            if (data == null)
                return;

            _context.Pembayarans.Remove(data);

            _context.SaveChanges();
        }

        private string GenerateNomor()
        {
            return "BYR" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}