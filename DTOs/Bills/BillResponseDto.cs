namespace GradDemo.DTOs.Bills
{
    public class BillItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class BillResponseDto
    {
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string CashierName { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public List<BillItemDto> Items { get; set; } = new();
    }
}