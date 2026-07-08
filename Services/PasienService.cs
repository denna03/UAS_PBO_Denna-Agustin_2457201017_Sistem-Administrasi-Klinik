using ClinicAdminApp.Data;
using ClinicAdminApp.Models;

namespace ClinicAdminApp.Services
{
    public class PasienService
    {
        private readonly ApplicationDbContext _context;

        public PasienService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Menampilkan semua data pasien
        public List<Pasien> GetAll()
        {
            return _context.Pasiens
                .OrderBy(x => x.NamaPasien)
                .ToList();
        }

        // Mengambil data berdasarkan ID
        public Pasien? GetById(Guid id)
        {
            return _context.Pasiens.Find(id);
        }

        // Menambahkan data pasien
        public void Create(Pasien pasien)
        {
            _context.Pasiens.Add(pasien);
            _context.SaveChanges();
        }

        // Mengubah data pasien
        public void Update(Pasien pasien)
        {
            _context.Pasiens.Update(pasien);
            _context.SaveChanges();
        }

        // Menghapus data pasien
        public void Delete(Guid id)
        {
            var pasien = _context.Pasiens.Find(id);

            if (pasien != null)
            {
                _context.Pasiens.Remove(pasien);
                _context.SaveChanges();
            }
        }

        // Pencarian pasien
        public List<Pasien> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAll();
            }

            return _context.Pasiens
                .Where(x =>
                    x.NamaPasien.Contains(keyword) ||
                    x.NoRM.Contains(keyword))
                .OrderBy(x => x.NamaPasien)
                .ToList();
        }
    }
}