using CommonServicesLib.Contracts;
using CommonServicesLib.DTOs;
using CommonServicesLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //private readonly ICartService _cartService;
        private readonly ICacheService _cacheService;
        private readonly IShoppingCartService _cartService;

        public CartController(ICacheService cacheService, IShoppingCartService cartService)
        {
            _cacheService = cacheService;
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(ShoppingCartDto cartDto)
        {
            await _cartService.CreateCartAsync(cartDto);
            return CreatedAtAction(nameof(GetCart), new { userId = cartDto.UserId }, cartDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCart(string userId, ShoppingCartDto cartDto)
        {
            if (userId != cartDto.UserId)
            {
                return BadRequest();
            }

            await _cartService.UpdateCartAsync(cartDto);
            return NoContent();
        }
        /*
        [HttpPost("{userId}/add")]
        public IActionResult AddToCart(string userId, CartItem cartItem)
        {
            _cartService.AddToCart(userId, cartItem);
            return NoContent();
        }
        */
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCart(string userId)
        {
            await _cartService.DeleteCartAsync(userId);
            return NoContent();
        }

        /*
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
        */
    }
}
