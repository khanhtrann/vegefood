using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vegefood.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Compare("Zip", ErrorMessage = "The is an invalid zip code")]
        public string Zip { get; set; }
    }
}
