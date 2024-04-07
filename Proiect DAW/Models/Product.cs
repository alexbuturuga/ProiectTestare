using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(50, ErrorMessage = "Product Name cannot exceed 50 characters.")]
        public string? ProductName { get; set; }

        [MaxLength(200, ErrorMessage = "Product Description cannot exceed 200 characters.")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Cover is required.")]
        [Url(ErrorMessage = "Invalid URL format for Cover.")]
        public string Cover { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal Rating { get; set; }
        public DateTime AddDate { get; set; }

    }
}