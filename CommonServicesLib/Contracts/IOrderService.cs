using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersByUserId(string userId);
        void CreateOrder(Order order);
    }
}
