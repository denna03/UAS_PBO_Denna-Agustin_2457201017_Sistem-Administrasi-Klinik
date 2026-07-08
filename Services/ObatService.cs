using ClinicAdminApp.Data;
using ClinicAdminApp.Models;

namespace ClinicAdminApp.Services
{
    public class ObatService
    {
        private readonly ApplicationDbContext _context;

        public ObatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Obat> GetAll()
        {
            return _context.Obats
                .OrderBy(x => x.NamaObat)
                .ToList();
        }

        public Obat? GetById(Guid id)
        {
            return _context.Obats.Find(id);
        }

        public void Create(Obat obat)
        {
            _context.Obats.Add(obat);
            _context.SaveChanges();
        }

        public void Update(Obat obat)
        {
            _context.Obats.Update(obat);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var obat = _context.Obats.Find(id);

            if (obat != null)
            {
                _context.Obats.Remove(obat);
                _context.SaveChanges();
            }
        }

        public List<Obat> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _context.Obats
                .Where(x =>
                    x.NamaObat.Contains(keyword) ||
                    x.KodeObat.Contains(keyword))
                .OrderBy(x => x.NamaObat)
                .ToList();
        }
    }
}