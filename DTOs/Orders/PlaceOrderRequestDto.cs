using System.ComponentModel.DataAnnotations;

namespace GradDemo.DTOs.Orders
{
    public class PlaceOrderRequestDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int CashierId { get; set; }

        [Required]
        public int CaptainId { get; set; }

        [Required]
        public int WaiterId { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        [MinLength(1)]
        public List<OrderItemDto> Items { get; set; } = new();
    }
}