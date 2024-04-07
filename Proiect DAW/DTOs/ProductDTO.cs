using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(50, ErrorMessage = "Product Name cannot exceed 50 characters.")]
        public string? ProductName { get; set; }

        [MaxLength(200, ErrorMessage = "Product Description cannot exceed 500 characters.")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Cover is required.")]
        [Url(ErrorMessage = "Invalid URL format for Cover.")]
        public string Cover { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal Rating { get; set; }
    }
}
