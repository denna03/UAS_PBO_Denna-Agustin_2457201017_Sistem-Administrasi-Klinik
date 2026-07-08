using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAdminApp.Models
{
    public class Pendaftaran
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nomor Pendaftaran")]
        public string NoPendaftaran { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tanggal Berobat")]
        public DateTime TanggalBerobat { get; set; }

        [Required]
        [Display(Name = "Keluhan")]
        public string Keluhan { get; set; } = string.Empty;

        [Required]
        public Guid PasienId { get; set; }

        [ForeignKey("PasienId")]
        public Pasien? Pasien { get; set; }

        [Required]
        public Guid DokterId { get; set; }

        [ForeignKey("DokterId")]
        public Dokter? Dokter { get; set; }

        [Required]
        public Guid PoliId { get; set; }

        [ForeignKey("PoliId")]
        public Poli? Poli { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Menunggu";
    }
}
