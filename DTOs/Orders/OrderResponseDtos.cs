namespace GradDemo.DTOs.Orders
{
    public class OrderItemResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? SizeCode { get; set; }
    }

    public class OrderResponseDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public double Total { get; set; }
        public string? CustomerName { get; set; }
        public string? CashierName { get; set; }
        public string? WaiterName { get; set; }
        public string? CaptainName { get; set; }
        public int TableId { get; set; }
        public string? TableArea { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }

    public class PagedOrdersResponseDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PaidOrders { get; set; }
        public int UnPaidOrders { get; set; }
        public List<OrderResponseDto> Orders { get; set; } = new();
    }
}