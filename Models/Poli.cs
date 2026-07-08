using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Poli
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Kode Poli")]
        public string KodePoli { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Poli")]
        public string NamaPoli { get; set; } = string.Empty;

        [Display(Name = "Lokasi")]
        public string Lokasi { get; set; } = string.Empty;

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; } = string.Empty;
    }
}