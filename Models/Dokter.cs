using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Dokter
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nama dokter wajib diisi")]
        [StringLength(100)]
        public string NamaDokter { get; set; } = string.Empty;

        [Required(ErrorMessage = "Spesialis wajib diisi")]
        [StringLength(100)]
        public string Spesialis { get; set; } = string.Empty;

        [Required(ErrorMessage = "No STR wajib diisi")]
        [StringLength(50)]
        public string NoSTR { get; set; } = string.Empty;

        [Phone]
        public string? NoTelepon { get; set; }

        [Required(ErrorMessage = "Jadwal praktek wajib diisi")]
        public string JadwalPraktek { get; set; } = string.Empty;
    }
}