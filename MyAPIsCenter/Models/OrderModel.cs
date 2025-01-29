namespace YourNamespace.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}