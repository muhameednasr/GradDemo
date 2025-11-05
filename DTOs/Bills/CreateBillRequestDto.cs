using System.ComponentModel.DataAnnotations;

namespace GradDemo.DTOs.Bills
{
    public class CreateBillRequestDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CashierId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}