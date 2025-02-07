using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Services.Kafka
{
    public class KafkaProducerService
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;

            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessageAsync(string message)
        {
            await _producer.ProduceAsync(_kafkaSettings.CartTopic, new Message<Null, string> { Value = message });
        }
    }
}
