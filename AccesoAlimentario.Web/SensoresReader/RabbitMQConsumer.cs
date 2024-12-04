using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AccesoAlimentario.Web.SensoresReader
{
    public class RabbitMQConsumer
    {
        private readonly string _queueName;
        private readonly Func<string, Task> _messageProcessor;
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQConsumer(IServiceScopeFactory scopeFactory, string queueName, Func<string, Task> messageProcessor)
        {
            _queueName = queueName;
            _messageProcessor = messageProcessor;
            _scopeFactory = scopeFactory;
            var factory = new ConnectionFactory
            {
                HostName = "localhost" // Adjust as necessary
            };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            // Ensure the queue exists
            _channel.QueueDeclareAsync(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            ).Wait();
        }

        public async void StartConsuming()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received: {message}");

                try
                {
                    await _messageProcessor(message);

                    // Acknowledge the message
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    // Optionally: Add retry logic or logging
                }
            };

            // Associate the consumer with the queue
            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false,
                consumer: consumer
            );
        }

        public async void StopConsuming()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}
