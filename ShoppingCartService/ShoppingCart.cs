using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CommonServicesLib.Models
{
    public class ShoppingCartItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }
        // Additional properties can be added here
    }
    public class ShoppingCart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ShoppingCartId { get; set; }

        public string UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        // Additional properties can be added here

    }
}
