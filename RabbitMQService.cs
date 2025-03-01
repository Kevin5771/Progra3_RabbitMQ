using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDemo
{
    public class RabbitMQService : IQueueService
    {
        private readonly string _queueName = "testQueue";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            try
            {
                _channel.QueueDeclarePassive(_queueName);
            }
            catch (RabbitMQ.Client.Exceptions.OperationInterruptedException)
            {
                _channel.QueueDeclare(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
            }
        }


        public void Enqueue(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);


            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: properties,
                body: body
            );

            Console.WriteLine($"[x] Sent: {message}");
        }


        public string Dequeue()
        {
            var result = _channel.BasicGet(_queueName, autoAck: true);
            if (result == null)
            {
                return "No messages in queue";
            }

            string message = Encoding.UTF8.GetString(result.Body.ToArray());
            Console.WriteLine($"[x] Received: {message}");
            return message;
        }



        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
