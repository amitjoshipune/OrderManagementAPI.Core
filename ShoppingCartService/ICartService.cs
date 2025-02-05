using CommonServicesLib.DTOs;

namespace ShoppingCartService
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartAsync(string userId);
        Task CreateCartAsync(ShoppingCartDto cartDto);
        Task UpdateCartAsync(ShoppingCartDto cartDto);
        Task DeleteCartAsync(string userId);
    }
}
