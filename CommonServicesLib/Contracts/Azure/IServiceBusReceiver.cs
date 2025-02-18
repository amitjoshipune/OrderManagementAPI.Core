namespace CommonServicesLib.Contracts.Azure
{
    public interface IServiceBusReceiver
    {
        public Task StartProcessingAsync();
        public Task StopProcessingAsync();
    }
}