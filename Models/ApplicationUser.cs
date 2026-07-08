using Microsoft.AspNetCore.Identity;

namespace ClinicAdminApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NamaLengkap { get; set; } = string.Empty;

        public string RoleName { get; set; } = "Petugas";
    }
}