using CommonServicesLib;
using CommonServicesLib.Contracts;
using CommonServicesLib.DTOs;
using CommonServicesLib.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ShoppingCartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //private readonly ICartService _cartService;
        private readonly ICacheService _cacheService;
        private readonly IShoppingCartService _cartService;

        private readonly IProducer<Null, string> _producer;
        private readonly KafkaSettings _kafkaSettings;

        private readonly IConsumer<Null, string> _consumer;

        public CartController(ICacheService cacheService, IShoppingCartService cartService , IOptions<KafkaSettings> kafkaSettings)
        {
            _cacheService = cacheService;
            _cartService = cartService;

            /* ---*/
            _kafkaSettings = kafkaSettings.Value;
            ////////////////////////////////////////////////////////
            var configProducer = new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(configProducer).Build();
            ////////////////////////////////////////////////////////
            var configConsumer = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = "kafka-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(configConsumer).Build();
            _consumer.Subscribe(_kafkaSettings.CartTopic);
            ////////////////////////////////////////////////////////
            /* ---*/
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var b = _kafkaSettings.BootstrapServers;
            var t = _kafkaSettings.CartTopic;
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(ShoppingCartDto cartDto)
        {
            await _cartService.CreateCartAsync(cartDto);
            
            var message = $"CreateCart called with : {cartDto.ToString()}";

            await _producer.ProduceAsync(_kafkaSettings.CartTopic, new Message<Null, string> { Value = message });

            return CreatedAtAction(nameof(GetCart), new { userId = cartDto.UserId }, cartDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCart(string userId, ShoppingCartDto cartDto)
        {
            if (userId != cartDto.UserId)
            {
                return BadRequest();
            }

            await _cartService.UpdateCartAsync(cartDto);
            return NoContent();
        }
        /*
        [HttpPost("{userId}/add")]
        public IActionResult AddToCart(string userId, CartItem cartItem)
        {
            _cartService.AddToCart(userId, cartItem);
            return NoContent();
        }
        */
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCart(string userId)
        {
            await _cartService.DeleteCartAsync(userId);
            return NoContent();
        }

        [HttpPost("{userId}/checkout")]
        public async Task<IActionResult> Checkout(string userId, CheckoutDto checkoutDto)
        {
            var result = await _cartService.CheckoutAsync(checkoutDto);
            return Ok(result);
        }

            /*
            [HttpDelete("{userId}/remove/{bookId}")]
            public IActionResult RemoveFromCart(string userId, string bookId)
            {
                _cartService.RemoveFromCart(userId, bookId);
                return NoContent();
            }

            [HttpPost("{userId}/checkout")]
            public IActionResult Checkout(string userId)
            {
                _cartService.Checkout(userId);
                return NoContent();
            }
            */
        }
    }
