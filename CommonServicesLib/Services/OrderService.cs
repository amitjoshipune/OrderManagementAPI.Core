using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new List<Order>();
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Orders";

        public OrderService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            if (!_cache.TryGetValue(CacheKey, out List<Order> orders))
            {
                orders = _orders;
                _cache.Set(CacheKey, orders);
            }
            return orders.Where(o => o.UserId == userId);
        }

        public void CreateOrder(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.UtcNow;
            _orders.Add(order);
            _cache.Set(CacheKey, _orders);
        }
    }
}
