using ClinicAdminApp.Data;
using ClinicAdminApp.Models;

namespace ClinicAdminApp.Services
{
    public class PoliService
    {
        private readonly ApplicationDbContext _context;

        public PoliService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Poli> GetAll()
        {
            return _context.Polis
                .OrderBy(x => x.NamaPoli)
                .ToList();
        }

        public Poli? GetById(Guid id)
        {
            return _context.Polis.Find(id);
        }

        public void Create(Poli poli)
        {
            _context.Polis.Add(poli);
            _context.SaveChanges();
        }

        public void Update(Poli poli)
        {
            _context.Polis.Update(poli);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var poli = _context.Polis.Find(id);

            if (poli != null)
            {
                _context.Polis.Remove(poli);
                _context.SaveChanges();
            }
        }

        public List<Poli> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return _context.Polis
                .Where(x =>
                    x.NamaPoli.Contains(keyword) ||
                    x.KodePoli.Contains(keyword))
                .OrderBy(x => x.NamaPoli)
                .ToList();
        }
    }
}