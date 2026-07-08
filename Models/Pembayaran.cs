using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Pembayaran
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string NoPembayaran { get; set; } = string.Empty;

        [Required]
        public Guid ResepId { get; set; }

        public Resep? Resep { get; set; }

        [Display(Name = "Biaya Pemeriksaan")]
        public decimal BiayaPemeriksaan { get; set; }

        [Display(Name = "Biaya Obat")]
        public decimal BiayaObat { get; set; }

        [Display(Name = "Total Bayar")]
        public decimal TotalBayar { get; set; }

        [Required]
        [Display(Name = "Metode Pembayaran")]
        public string MetodePembayaran { get; set; } = "Tunai";

        public DateTime TanggalPembayaran { get; set; } = DateTime.Now;

        [Display(Name = "Status Pembayaran")]
        public string Status { get; set; } = "Lunas";
    }
}