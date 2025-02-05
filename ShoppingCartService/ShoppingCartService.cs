using AutoMapper;
using CommonServicesLib.DTOs;
using CommonServicesLib.Models;

namespace ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMapper _mapper;


        public ShoppingCartService(IShoppingCartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task CreateCartAsync(ShoppingCartDto cartDto)
        {
            var cart = _mapper.Map<ShoppingCart>(cartDto);
            await _cartRepository.CreateCartAsync(cart);
        }

        public async Task DeleteCartAsync(string userId)
        {
            await _cartRepository.DeleteCartAsync(userId);
        }

        public async Task<ShoppingCartDto> GetCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartAsync(userId);
            return _mapper.Map<ShoppingCartDto>(cart);
        }

        public async Task UpdateCartAsync(ShoppingCartDto cartDto)
        {
            var cart = _mapper.Map<ShoppingCart>(cartDto);
            await _cartRepository.UpdateCartAsync(cart);
        }
    }
}
