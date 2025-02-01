using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICacheService _cacheService;
        public OrdersController(ICacheService cacheService, IOrderService orderService)
        {
            _cacheService = cacheService;
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(string userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrdersByUserId), new { userId = order.UserId }, order);
        }
    }
}
