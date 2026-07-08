using ClinicAdminApp.Data;
using ClinicAdminApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAdminApp.Services
{
    public class PendaftaranService
    {
        private readonly ApplicationDbContext _context;

        public PendaftaranService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Pendaftaran> GetAll()
        {
            return _context.Pendaftarans
                .Include(x => x.Pasien)
                .Include(x => x.Dokter)
                .Include(x => x.Poli)
                .OrderByDescending(x => x.TanggalBerobat)
                .ToList();
        }

        public Pendaftaran? GetById(Guid id)
        {
            return _context.Pendaftarans
                .Include(x => x.Pasien)
                .Include(x => x.Dokter)
                .Include(x => x.Poli)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Create(Pendaftaran data)
        {
            _context.Pendaftarans.Add(data);
            _context.SaveChanges();
        }

        public void Update(Pendaftaran data)
        {
            _context.Pendaftarans.Update(data);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var data = _context.Pendaftarans.Find(id);

            if (data != null)
            {
                _context.Pendaftarans.Remove(data);
                _context.SaveChanges();
            }
        }
    }
}