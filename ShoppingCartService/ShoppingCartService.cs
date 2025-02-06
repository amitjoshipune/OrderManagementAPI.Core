using AutoMapper;
using CommonServicesLib.Contracts;
using CommonServicesLib.DTOs;
using CommonServicesLib.Models;


namespace ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private CommonServicesLib.RabbitMqClient _rabbitMqClient;

        public ShoppingCartService(IShoppingCartRepository cartRepository, IMapper mapper , IOrderService orderService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<bool> CheckoutAsync(CheckoutDto checkoutDto)
        {
            var cart = await GetCartByUserIdAsync(checkoutDto.UserId);
            if (cart == null)
            {
                return false;
            }

            // Map Cart to CheckoutDto
            checkoutDto.Items = cart.Items.Select(i => new CheckoutItemDto
            {
                ProductId = i.BookId,
                Quantity = i.Quantity,
                Price = 10 // Assuming a static price for simplicity
            }).ToList();

            checkoutDto.TotalAmount = cart.Items.Sum(i => i.Quantity * 10); // Assuming a static price for simplicity

            // Here you would process the payment and shipping details
            // For example:
            var paymentSuccessful = ProcessPayment(checkoutDto.PaymentMethod, checkoutDto.TotalAmount);
            if (!paymentSuccessful)
            {
                return false;
            }

            // Create order via OrderService
            var orderDto = new OrderDto
            {
                UserId = checkoutDto.UserId,
                Items = checkoutDto.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList(),
                TotalAmount = checkoutDto.TotalAmount
            };

            var order = await _orderService.CreateOrderAsync(orderDto);

            // Publish order creation event to RabbitMQ
            _rabbitMqClient.Publish("orderQueue", $"Order Created: {order.Id}");

            // Clear the cart
            var result = await ClearCartAsync(checkoutDto.UserId);
            return result;
        }

        private bool ProcessPayment(string paymentMethod, decimal amount)
        {
            // Implement your payment processing logic here
            // Return true if payment is successful, otherwise false
            return true; // For simplicity, assuming payment is always successful
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            throw new NotImplementedException();
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

        public async Task<ShoppingCartDto> GetCartByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartAsync(userId);
            return _mapper.Map<ShoppingCartDto>(cart);
        }

        public async Task UpdateCartAsync(ShoppingCartDto cartDto)
        {
            var cart = _mapper.Map<ShoppingCart>(cartDto);
            // Ensure cart.Id is not modified
            var existingCart = await _cartRepository.GetCartAsync(cart.UserId);
            cart.ShoppingCartId = existingCart.ShoppingCartId;

            await _cartRepository.UpdateCartAsync(cart);
        }
    }
}
