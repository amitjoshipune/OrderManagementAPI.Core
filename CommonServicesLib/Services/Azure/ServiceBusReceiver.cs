using Azure.Messaging.ServiceBus;
using CommonServicesLib.Contracts.Azure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Services.Azure
{
    public class ServiceBusReceiver : IServiceBusReceiver
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private ServiceBusClient _serviceBusclient;
        private ServiceBusProcessor _serviceBusProcessor;
        public ServiceBusReceiver(string connectionString, string queueName)
        {
            this._connectionString = connectionString;
            this._queueName = queueName;
            _serviceBusclient = new ServiceBusClient(connectionString);
        }

        public async Task StartProcessingAsync()
        {
            _serviceBusProcessor = _serviceBusclient.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            _serviceBusProcessor.ProcessMessageAsync += MessageHandlerAsync;
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandlerAsync;

            Console.WriteLine($"Info: StartProcessingAsync calling");
            Debug.WriteLine($"Info: StartProcessingAsync calling");
            await _serviceBusProcessor.StartProcessingAsync();
            Console.WriteLine($"Info: StartProcessingAsync called");
            Debug.WriteLine($"Info: StartProcessingAsync called");
        }

        public async Task StopProcessingAsync()
        {
            await _serviceBusProcessor.StopProcessingAsync();
            await _serviceBusProcessor.DisposeAsync();
        }

        private async Task MessageHandlerAsync(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Debug.WriteLine($"Received message: {body}");
            Console.WriteLine($"Received message: {body}");

            // TODO add actual message processing over here like send email , call azure function, or call API, save to DB etc.

            // Complete the message
            Console.WriteLine($"Info: CompleteMessageAsync calling");
            Debug.WriteLine($"Info: CompleteMessageAsync calling");
            await args.CompleteMessageAsync(args.Message);
            Console.WriteLine($"Info: CompleteMessageAsync called");
            Debug.WriteLine($"Info: CompleteMessageAsync called");
        }

        private Task ErrorHandlerAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            Debug.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
