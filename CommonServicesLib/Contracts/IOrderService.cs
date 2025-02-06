using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<bool> DeleteOrderAsync(string id);
        Task<OrderDto> GetOrderByIdAsync(string id);
        Task<bool> UpdateOrderAsync(string id, OrderDto orderDto);
    }
}