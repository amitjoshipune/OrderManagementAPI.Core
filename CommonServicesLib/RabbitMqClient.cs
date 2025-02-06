using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib
{
    public class RabbitMqClient : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqClient(string hostname)
        {
            var factory = new ConnectionFactory() { HostName = hostname };
            //_connection = factory.CreateConnection();
            //_channel = _connection.CreateModel();
        }

        public void Publish(string queueName, string message)
        {
            //_channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //var body = Encoding.UTF8.GetBytes(message);
            //_channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            //_channel.Close();
            //_connection.Close();
        }
    }

}
