using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
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