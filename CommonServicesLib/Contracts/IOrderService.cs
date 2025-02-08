using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<bool> DeleteOrderAsync(string orderid);
        Task<OrderDto?> GetOrderByIdAsync(string orderid);
        Task<IEnumerable<OrderDto>?> GetOrderByUserIdAsync(string userId);
        Task<bool> UpdateOrderAsync(string orderid, OrderDto orderDto);
    }
}