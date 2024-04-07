using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class ReceiptProduct
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Receipt ID is required.")]
        public int ReceiptId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public int Amount { get; set; }
        public virtual Product Product {get; set;}
        public virtual Receipt Receipt { get; set; }
    }
}
