using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.DTOs
{
    public class PromotionDTO
    {
        [MaxLength(200, ErrorMessage = "Promotion Description cannot exceed 200 characters.")]
        public string? PromotionDescription { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public int Discount { get; set; }
    }
}
