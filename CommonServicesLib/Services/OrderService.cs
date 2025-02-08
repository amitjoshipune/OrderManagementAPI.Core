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

        public async Task<OrderDto?> GetOrderByIdAsync(string orderid)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderid);
            return MapToOrderDto(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = MapToOrder(orderDto);
            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return MapToOrderDto(createdOrder);
        }

        public async Task<bool> UpdateOrderAsync(string orderid, OrderDto orderDto)
        {
            var order = MapToOrder(orderDto);
            order.Id = orderid;
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(string orderid)
        {
            return await _orderRepository.DeleteOrderAsync(orderid);
        }

        public async Task<IEnumerable<OrderDto>?> GetOrderByUserIdAsync(string userId)
        {
            var OrderDtos = new List<OrderDto>();

            var ordersByUser = await _orderRepository.GetOrdersByUserIdAsync(userId);
            if (ordersByUser != null && ordersByUser.Any())
            {
                foreach (var aOrder in ordersByUser)
                {
                    OrderDtos.Add(MapToOrderDto(aOrder));
                }
                return OrderDtos;
            }
            else
            {
                return null;
            }
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
