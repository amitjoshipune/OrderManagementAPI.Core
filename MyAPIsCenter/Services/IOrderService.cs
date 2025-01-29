using YourNamespace.Models;
using System.Collections.Generic;

namespace YourNamespace.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersByUserId(string userId);
        void CreateOrder(Order order);
    }
}