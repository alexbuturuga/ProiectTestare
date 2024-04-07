using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.DTOs
{
    public class AccountPromotionDTO
    {

        [Required(ErrorMessage = "Account ID is required.")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Promotion ID is required.")]
        public int PromotionId { get; set; }
    }
}
