using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface ICartService
    {
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, CartItem cartItem);
        void RemoveFromCart(string userId, string bookId);
        void Checkout(string userId);
    }
}
