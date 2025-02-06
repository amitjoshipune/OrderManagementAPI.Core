using CommonServicesLib.Contracts;
using CommonServicesLib.Models;

namespace OrderService
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid().ToString(); //_orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            _orders.Add(order);
            return await Task.FromResult(order);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder == null)
            {
                return await Task.FromResult(false);
            }
            existingOrder.Items = order.Items;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.OrderDate = order.OrderDate;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return await Task.FromResult(false);
            }
            _orders.Remove(order);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await Task.FromResult(_orders.Where(o => o.UserId == userId).ToList());
        }
    }

}
