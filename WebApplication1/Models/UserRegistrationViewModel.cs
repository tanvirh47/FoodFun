using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required] 
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
