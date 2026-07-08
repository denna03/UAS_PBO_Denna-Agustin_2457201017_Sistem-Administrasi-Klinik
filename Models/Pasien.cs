using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models
{
    public class Pasien
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "No Rekam Medis")]
        public string NoRM { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nama Pasien")]
        public string NamaPasien { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Jenis Kelamin")]
        public string JenisKelamin { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tanggal Lahir")]
        public DateTime TanggalLahir { get; set; }

        [Required]
        [Display(Name = "Alamat")]
        public string Alamat { get; set; } = string.Empty;

        [Required]
        [Display(Name = "No Telepon")]
        public string NoTelepon { get; set; } = string.Empty;
    }
}