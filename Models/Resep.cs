using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Resep
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string NomorResep { get; set; } = string.Empty;

        [Required]
        public Guid PemeriksaanId { get; set; }

        public Pemeriksaan? Pemeriksaan { get; set; }

        public DateTime TanggalResep { get; set; } = DateTime.Now;

        public decimal TotalBiaya { get; set; }

        public ICollection<DetailResep>? DetailReseps { get; set; }
    }
}