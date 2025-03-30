using Microsoft.AspNetCore.Identity;

namespace Custom_Identity_Auth.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string IdNo { get; set; }
    }
}
