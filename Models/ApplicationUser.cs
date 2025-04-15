using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Custom_Identity_Auth.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(30,MinimumLength =4)]
         public string IdNo { get; set; }
    }
}
