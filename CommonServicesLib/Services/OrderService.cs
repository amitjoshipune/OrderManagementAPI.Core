using CommonServicesLib.Contracts;
using CommonServicesLib.Models;

namespace CommonServicesLib.Services
{
    public class OrdersService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> GetOrderByIdAsync(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return MapToOrderDto(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = MapToOrder(orderDto);
            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return MapToOrderDto(createdOrder);
        }

        public async Task<bool> UpdateOrderAsync(string id, OrderDto orderDto)
        {
            var order = MapToOrder(orderDto);
            order.Id = id;
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }

        private Order MapToOrder(OrderDto orderDto)
        {
            return new Order
            {
                Id = orderDto.Id,
                UserId = orderDto.UserId,
                Items = orderDto.Items.Select(i => new OrderItem { ProductId = i.ProductId, Quantity = i.Quantity, Price = i.Price }).ToList(),
                TotalAmount = orderDto.TotalAmount
            };
        }

        private OrderDto MapToOrderDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Items = order.Items.Select(i => new OrderItemDto { ProductId = i.ProductId, Quantity = i.Quantity, Price = i.Price }).ToList(),
                TotalAmount = order.TotalAmount
            };
        }


    }

}
