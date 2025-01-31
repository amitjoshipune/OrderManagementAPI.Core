using YourNamespace.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Services
{
    public class CartService : ICartService
    {
        private readonly List<Cart> _carts = new List<Cart>();
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Carts";

        public CartService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Cart GetCartByUserId(string userId)
        {
            if (!_cache.TryGetValue(CacheKey, out List<Cart> carts))
            {
                carts = _carts;
                _cache.Set(CacheKey, carts);
            }
            return carts.FirstOrDefault(c => c.UserId == userId) ?? new Cart { UserId = userId };
        }

        public void AddToCart(string userId, CartItem cartItem)
        {

            if (!_cache.TryGetValue(CacheKey, out List<Cart> carts))
            {
                carts = _carts;
                _cache.Set(CacheKey, carts);
            }

            if (carts == null || carts.Count == 0)
            {
                carts = new List<Cart>();
                _cache.Set(CacheKey, carts);
            }

            var userCarts = carts.FirstOrDefault(c => c.UserId == userId);
            if (userCarts == null)
            {
                var aNewUserCart = new Cart { UserId = userId };
                aNewUserCart.Items.Add(cartItem);
                carts.Add(aNewUserCart);
            }
            else
            {
                var existingItem = userCarts.Items.FirstOrDefault(i => i.BookId == cartItem.BookId);
                if (existingItem != null)
                {
                    existingItem.Quantity += cartItem.Quantity;
                }
                else
                {
                    userCarts.Items.Add(cartItem);
                }
            }

            //// Do this for in memory list of carts
            var _userCarts = _carts.FirstOrDefault(c => c.UserId == userId);
            if (_userCarts == null)
            {
                var aNewUserCart = new Cart { UserId = userId };
                aNewUserCart.Items.Add(cartItem);
                _carts.Add(aNewUserCart);
            }
            else
            {
                var existingItem = _userCarts.Items.FirstOrDefault(i => i.BookId == cartItem.BookId);
                if (existingItem != null)
                {
                    existingItem.Quantity += cartItem.Quantity;
                }
                else
                {
                    _userCarts.Items.Add(cartItem);
                }
            }
            _cache.Set(CacheKey, _carts);

            /*
            var cart = GetCartByUserId(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == cartItem.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.Items.Add(cartItem);
            }
            _cache.Set(CacheKey, _carts);
            */
        }

        public void RemoveFromCart(string userId, string bookId)
        {
            var cart = GetCartByUserId(userId);
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                cart.Items.Remove(item);
                _cache.Set(CacheKey, _carts);
            }
        }

        public void Checkout(string userId)
        {
            var cart = GetCartByUserId(userId);
            // Process the order (e.g., save to database, send confirmation email, etc.)
            _carts.Remove(cart);
            _cache.Set(CacheKey, _carts);
        }
    }
}