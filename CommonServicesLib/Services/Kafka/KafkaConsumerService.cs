using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Services.Kafka
{
    public class KafkaConsumerService
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumerService(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;

            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = "kafka-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
            _consumer.Subscribe(_kafkaSettings.CartTopic);
        }

        public void ConsumeMessages(Action<string> messageHandler)
        {
            while (true)
            {
                var result = _consumer.Consume();
                messageHandler(result.Message.Value);
            }
        }
    }
}

/*
 * [ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly KafkaConsumerService _kafkaConsumerService;

    public OrderController(KafkaConsumerService kafkaConsumerService)
    {
        _kafkaConsumerService = kafkaConsumerService;
    }

    [HttpGet]
    public IActionResult ProcessOrder()
    {
        _kafkaConsumerService.ConsumeMessages(message =>
        {
            // Process the message (e.g., create an order)
            Console.WriteLine($"Processing message: {message}");
        });

        return Ok("Processing order");
    }
}
*/

/*
 [ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly KafkaProducerService _kafkaProducerService;

    public ShoppingCartController(KafkaProducerService kafkaProducerService)
    {
        _kafkaProducerService = kafkaProducerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] string product)
    {
        await _kafkaProducerService.SendMessageAsync($"Product added to cart: {product}");
        return Ok("Product added to cart");
    }
}

*/