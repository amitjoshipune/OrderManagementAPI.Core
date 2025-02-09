using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using MongoDB.Driver;

namespace OrderService
{
    public class OrderRepository : IOrderRepository
    {
        //private readonly List<Order> _orders = new List<Order>();
        private readonly IMongoCollection<Order?>? _orders;
        private readonly MongoDBContext _context;

        public OrderRepository(MongoDBContext context)
        {
            _orders = context.Orders;
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(string orderid)
        {
            //return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == orderid));
            return await _orders.Find(filter: o => o.Id == orderid).FirstOrDefaultAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid().ToString(); //_orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            await _orders.InsertOneAsync(order);
            return await Task.FromResult(order);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = Builders<Order>.Filter.Eq("Id",order.Id);


            if (existingOrder == null)
            {
                return await Task.FromResult(false);
            }

            var update = Builders<Order>.Update
                .Set(o => o.Items, order.Items)
                .Set(o => o.TotalAmount, order.TotalAmount)
                ;
            var ret = await _context.Orders.UpdateOneAsync(existingOrder, update);
            return await Task.FromResult(true);

        }

        public async Task<bool> DeleteOrderAsync(string orderid)
        {
            var existingOrder = await _orders.Find(order => order.Id == orderid).FirstOrDefaultAsync();


            if (existingOrder == null || string.IsNullOrEmpty(existingOrder.Id))
            {
                return await Task.FromResult(false);
            }

            var ret = _orders.DeleteOneAsync(o=>o.Id == orderid);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Order>?> GetOrdersByUserIdAsync(string userId)
        {
            return await _orders.Find(Order => Order.UserId == userId).ToListAsync();
        }
    }

}
