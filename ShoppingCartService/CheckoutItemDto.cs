namespace ShoppingCartService
{
    public class CheckoutItemDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}