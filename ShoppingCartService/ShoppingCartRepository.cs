using CommonServicesLib.Models;
using MongoDB.Driver;

namespace ShoppingCartService
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IMongoCollection<ShoppingCart> _carts;
        private readonly MongoDBContext _context;
        public ShoppingCartRepository(MongoDBContext context)
        {
            _carts = context.Carts;
            _context = context;
        }

        //public Task<bool> ClearCartAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

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
            // Specify fields to update without altering `_id`
            var update = Builders<ShoppingCart>.Update
                .Set(c => c.Items, cart.Items);

            await _context.Carts.UpdateOneAsync(filter, update);
            //await _carts.ReplaceOneAsync(filter, cart);
        }
    }
}
