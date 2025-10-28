using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace GradDemo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }

        //refers to customer who made order
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual User? Customer { get; set; }
        // refers to cashier who submitted order
        [ForeignKey(nameof(Cashier))]
        public int CashierId { get; set; }
        public virtual User? Cashier { get; set; }

        [ForeignKey(nameof(Captain))]
        public int CaptainId { get; set; }
        public virtual User? Captain { get; set; }

        [ForeignKey(nameof(Waiter))]
        public int WaiterId { get; set; }
        public virtual User? Waiter { get; set; }

        [ForeignKey(nameof(Table))]
        public int TableId { get; set; }
        public virtual Table? Table { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public void CalculateTotal()
        {
            Total = 0;
            foreach (var oi in OrderItems)
            {
                var itemSize= oi.Item.ItemSizes.FirstOrDefault(its=>its.SizeId==oi.SizeId);

                double multiplier = itemSize?.Multiplier ?? 1.0;
                Total += oi.Quantity * oi.Item.Price * multiplier;
            }
        }
    }
}
