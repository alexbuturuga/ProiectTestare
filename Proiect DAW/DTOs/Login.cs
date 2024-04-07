using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.DTOs
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public string Password { get; set; }
    }
}
