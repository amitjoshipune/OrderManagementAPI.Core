namespace CommonServicesLib.Models
{
    public class OrderDto
    {
        public string UserId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string Id { get; set; }
    }
}