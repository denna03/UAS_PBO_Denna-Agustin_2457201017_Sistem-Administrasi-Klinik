using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Obat
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Kode Obat")]
        public string KodeObat { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Obat")]
        public string NamaObat { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Kategori")]
        public string Kategori { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Satuan")]
        public string Satuan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Harga")]
        public decimal Harga { get; set; }

        [Required]
        [Display(Name = "Stok")]
        public int Stok { get; set; }

        [Required]
        [Display(Name = "Tanggal Kedaluwarsa")]
        public DateTime TanggalExpired { get; set; }
    }
}