namespace CommonServicesLib.DTOs
{
    public class ShoppingCartItemDto
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }

        // Additional properties can be added here
    }
    public class ShoppingCartDto
    {
        public string UserId { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();

        // Additional properties can be added here
    }
}
