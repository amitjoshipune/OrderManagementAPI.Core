namespace AzureServiceBusProcessing
{
    using Azure.Messaging.ServiceBus;
    using CommonServicesLib.Contracts.Azure;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class ServiceBusHostedService : IHostedService
    {
        private readonly IServiceBusReceiver _serviceBusReceiver;

        public ServiceBusHostedService(IServiceBusReceiver serviceBusReceiver)
        {
            _serviceBusReceiver = serviceBusReceiver;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _serviceBusReceiver.StartProcessingAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _serviceBusReceiver.StopProcessingAsync();
        }
    }

}
