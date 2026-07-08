using ClinicAdminApp.Data;
using ClinicAdminApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAdminApp.Services
{
    public class PemeriksaanService
    {
        private readonly ApplicationDbContext _context;

        public PemeriksaanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Pemeriksaan> GetAll()
        {
            return _context.Pemeriksaans
                .Include(x => x.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .Include(x => x.Pendaftaran)
                .ThenInclude(x => x.Dokter)
                .Include(x => x.Pendaftaran)
                .ThenInclude(x => x.Poli)
                .OrderByDescending(x => x.TanggalPemeriksaan)
                .ToList();
        }

        public Pemeriksaan? GetById(Guid id)
        {
            return _context.Pemeriksaans
                .Include(x => x.Pendaftaran)
                .ThenInclude(x => x.Pasien)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Create(Pemeriksaan data)
        {
            _context.Pemeriksaans.Add(data);

            var pendaftaran = _context.Pendaftarans.Find(data.PendaftaranId);

            if (pendaftaran != null)
            {
                pendaftaran.Status = "Selesai";
            }

            _context.SaveChanges();
        }

        public void Update(Pemeriksaan data)
        {
            _context.Pemeriksaans.Update(data);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var data = _context.Pemeriksaans.Find(id);

            if (data != null)
            {
                _context.Pemeriksaans.Remove(data);
                _context.SaveChanges();
            }
        }
    }
}