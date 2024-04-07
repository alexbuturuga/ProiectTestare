using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class AccountPromotion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Account ID is required.")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Promotion ID is required.")]
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual Account Account { get; set; }
    }
}
