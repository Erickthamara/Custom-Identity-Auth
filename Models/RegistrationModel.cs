using System.ComponentModel.DataAnnotations;

namespace Custom_Identity_Auth.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        required public string IdNo { get; set; }


        //[Required]
        [EmailAddress]
        required public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Password must contain only numbers.")]
        [Display(Name ="Pin")]
        required public string Password { get; set; }

        public string Gender { get; set; }
    }
}
