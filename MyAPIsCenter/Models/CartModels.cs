namespace YourNamespace.Models
{
    public class CartItem
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}