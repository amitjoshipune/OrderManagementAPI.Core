
namespace AuthenticationService.Services
{
    public interface IAzureServiceBusClient
    {
        public Task SendMessageAsync(string queueName, string mesage);
    }
}