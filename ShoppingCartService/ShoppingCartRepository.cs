using CommonServicesLib.Models;
using MongoDB.Driver;

namespace ShoppingCartService
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IMongoCollection<ShoppingCart> _carts;

        public ShoppingCartRepository(MongoDBContext context)
        {
            _carts = context.Carts;
        }
        public async Task CreateCartAsync(ShoppingCart cart)
        {
            await _carts.InsertOneAsync(cart);
        }

        public async Task DeleteCartAsync(string userId)
        {
            await _carts.DeleteOneAsync(cart=>cart.UserId == userId);
        }

        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
           return await _carts.Find(cart => cart.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            var filter = Builders<ShoppingCart>.Filter.Eq("UserId" , cart.UserId);
            await _carts.ReplaceOneAsync(filter, cart);
        }
    }
}
