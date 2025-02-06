using CommonServicesLib.DTOs;

namespace ShoppingCartService
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartByUserIdAsync(string userId);
        Task CreateCartAsync(ShoppingCartDto cartDto);
        Task UpdateCartAsync(ShoppingCartDto cartDto);
        Task DeleteCartAsync(string userId);
        Task<bool> ClearCartAsync(string userId);
        Task<bool> CheckoutAsync(CheckoutDto checkoutDto);
    }
}
