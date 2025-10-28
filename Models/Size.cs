namespace GradDemo.Models
{
    public class Size
    {
        public int Id { get; set; }
        public char Code { get; set; } // 'S', 'M', 'L'
        public string? Description { get; set; }

        public virtual ICollection<ItemSize> ItemSizes { get; set; }
    }
}
