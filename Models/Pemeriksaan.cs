using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAdminApp.Models
{
    public class Pemeriksaan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PendaftaranId { get; set; }

        [ForeignKey(nameof(PendaftaranId))]
        public Pendaftaran? Pendaftaran { get; set; }

        [Display(Name = "Tekanan Darah")]
        public string TekananDarah { get; set; } = string.Empty;

        [Display(Name = "Suhu Tubuh")]
        public decimal SuhuTubuh { get; set; }

        [Display(Name = "Berat Badan")]
        public decimal BeratBadan { get; set; }

        [Display(Name = "Tinggi Badan")]
        public decimal TinggiBadan { get; set; }

        [Required]
        [Display(Name = "Diagnosa")]
        public string Diagnosa { get; set; } = string.Empty;

        [Display(Name = "Catatan Dokter")]
        public string Catatan { get; set; } = string.Empty;

        public DateTime TanggalPemeriksaan { get; set; } = DateTime.Now;
    }
}