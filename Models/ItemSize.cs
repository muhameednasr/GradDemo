using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradDemo.Models
{
    public class ItemSize
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey(nameof(Size))]
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        public double Multiplier { get; set; }
    }
}
