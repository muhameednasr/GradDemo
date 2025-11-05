namespace GradDemo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ICollection<ItemSize> ItemSizes { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
