using System.ComponentModel.DataAnnotations;

namespace GradDemo.DTOs.Orders
{
    public class OrderItemDto
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public int SizeId { get; set; }
    }
}