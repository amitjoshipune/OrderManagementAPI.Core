using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace AuthenticationService.Services
{
    public class AzureServiceBusClient : IAzureServiceBusClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly string connectionString ;
        public AzureServiceBusClient(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("AzureServiceBusWithTopics:connectionString");
            _serviceBusClient = new ServiceBusClient(connectionString);
        }

        public async Task SendMessageAsync(string queueName, string mesage)
        {
            var sender = _serviceBusClient.CreateSender(queueName);

            var jsonMessage = JsonConvert.SerializeObject(mesage);

            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);

            await sender.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }

    }

}