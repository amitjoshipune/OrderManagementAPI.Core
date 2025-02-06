namespace ShoppingCartService
{
    public class CheckoutDto
    {
        public string UserId { get; set; }
        public List<CheckoutItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, etc.
        public string ShippingAddress { get; set; }
    }
}
