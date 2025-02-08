using CommonServicesLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Contracts
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(string orderid);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>?> GetOrdersByUserIdAsync(string userId);
        Task<bool> DeleteOrderAsync(string orderid);
    }
}
