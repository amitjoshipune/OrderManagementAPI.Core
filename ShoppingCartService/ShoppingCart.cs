using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ShoppingCartService
{
    public class ShoppingCartItem
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }
    }
    public class ShoppingCart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ShoppingCartId { get; set; }

        public string UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
