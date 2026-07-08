using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class DetailResep
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ResepId { get; set; }

        public Resep? Resep { get; set; }

        public Guid ObatId { get; set; }

        public Obat? Obat { get; set; }

        [Range(1,1000)]
        public int Jumlah { get; set; }

        public decimal Harga { get; set; }

        public decimal SubTotal { get; set; }
    }
}