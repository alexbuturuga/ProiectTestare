using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Admin status is required.")]
        [Range(typeof(bool), "false", "true", ErrorMessage = "Invalid value for Admin status.")]
        public bool Admin { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }

        public int Pinatas { get; set; }

        public void CalculatePinatas(int amount, int multiplier, int bonus)
        {
            if (amount <= 0 || multiplier <= 0)
            {
                throw new ArgumentException("Amount and multiplier must be greater than zero.");
            }

            int totalPinatas = 0;

            for (int i = 1; i <= amount; i++)
            {
                if (i % 10 == 0) // Every 10th piñata gets a bonus
                {
                    totalPinatas += multiplier + bonus;
                }
                else
                {
                    totalPinatas += multiplier;
                }
            }

            Pinatas = totalPinatas;
        }
    }
}
