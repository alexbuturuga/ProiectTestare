using Proiect_DAW.Models;
using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.DTOs
{
    public class AccountDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "confirmPassword is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Admin status is required.")]
        [Range(typeof(bool), "false", "true", ErrorMessage = "Invalid value for Admin status.")]
        public bool Admin { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }
    }
}
