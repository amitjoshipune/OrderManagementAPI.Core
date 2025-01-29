using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface ICartService
    {
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, CartItem cartItem);
        void RemoveFromCart(string userId, string bookId);
        void Checkout(string userId);
    }
}