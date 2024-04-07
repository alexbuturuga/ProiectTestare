using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sum is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Sum must be a non-negative value.")]
        public double Sum { get; set; }

        [MaxLength(200, ErrorMessage = "Receipt Description cannot exceed 200 characters.")]
        public string? ReceiptDescription { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Account ID is required.")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}