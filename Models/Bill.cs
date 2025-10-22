using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime BillDate { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


        //refers to cashier
        [ForeignKey(nameof(Cashier))]
        public int CashierId { get; set; }
        public virtual User Cashier { get; set; }
    }
}
