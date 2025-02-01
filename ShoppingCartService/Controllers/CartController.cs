using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICacheService _cacheService;

        public CartController(ICacheService cacheService, ICartService cartService)
        {
            _cacheService = cacheService;
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCartByUserId(string userId)
        {
            var cart = _cartService.GetCartByUserId(userId);
            return Ok(cart);
        }

        [HttpPost("{userId}/add")]
        public IActionResult AddToCart(string userId, CartItem cartItem)
        {
            _cartService.AddToCart(userId, cartItem);
            return NoContent();
        }

        [HttpDelete("{userId}/remove/{bookId}")]
        public IActionResult RemoveFromCart(string userId, string bookId)
        {
            _cartService.RemoveFromCart(userId, bookId);
            return NoContent();
        }

        [HttpPost("{userId}/checkout")]
        public IActionResult Checkout(string userId)
        {
            _cartService.Checkout(userId);
            return NoContent();
        }
    }
}
