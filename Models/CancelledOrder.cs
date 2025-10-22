using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class CancelledOrder
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime CancelledDate { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


        //refers to cashier
        [ForeignKey(nameof(CancelledBy))]
        public int CancelledById { get; set; }
        public virtual User CancelledBy { get; set; }
    }
}
