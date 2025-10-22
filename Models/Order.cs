using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

        //refers to customer who made order
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }
        // refers to cashier who submitted order
        [ForeignKey(nameof(Cashier))]
        public int CashierId { get; set; }
        public virtual User Cashier { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
