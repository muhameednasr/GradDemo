using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public bool IsPayed { get; set; } = false;

        [ForeignKey(nameof(Size))]
        public int SizeId { get; set; } 
        public virtual Size? Size { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item? Item { get; set; }
    }
}
