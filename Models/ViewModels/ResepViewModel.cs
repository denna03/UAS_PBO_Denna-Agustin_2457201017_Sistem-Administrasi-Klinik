using System.ComponentModel.DataAnnotations;

namespace ClinicAdminApp.Models.ViewModels
{
    public class ResepViewModel
    {
        [Required]
        public Guid PemeriksaanId { get; set; }

        [Required]
        public Guid ObatId { get; set; }

        [Range(1,100)]
        public int Jumlah { get; set; }
    }
}