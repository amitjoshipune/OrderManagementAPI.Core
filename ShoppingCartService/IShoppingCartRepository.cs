using CommonServicesLib.Models;

namespace ShoppingCartService
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartAsync(string userId);
        Task CreateCartAsync(ShoppingCart cart);
        Task UpdateCartAsync(ShoppingCart cart);
        Task DeleteCartAsync(string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
