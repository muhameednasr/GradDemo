using System.ComponentModel.DataAnnotations;

namespace GradDemo.DTOs.Orders
{
    public class UpdateOrderRequestDto
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
        public List<UpdateOrderItemDto> Items { get; set; } = new();
    }

    public class UpdateOrderItemDto : OrderItemDto
    {
        public bool IsPayed { get; set; }
    }
}