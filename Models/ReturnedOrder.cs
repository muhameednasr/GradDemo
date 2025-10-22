using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class ReturnedOrder
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

    }
}
