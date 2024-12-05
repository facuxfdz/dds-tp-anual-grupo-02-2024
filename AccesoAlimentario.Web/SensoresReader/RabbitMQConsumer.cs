using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AccesoAlimentario.Web.SensoresReader
{
    public class RabbitMqConsumer
    {
        private readonly string _queueName;
        private readonly Func<List<string>, Task> _messageProcessor;
        private readonly IConnection? _connection;
        private readonly IChannel? _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly List<string> _messageBuffer = new List<string>();
        private readonly Timer _flushTimer;
        private const int BufferIntervalSeconds = 5; // Intervalo en segundos para hacer el "flush"

        public RabbitMqConsumer(ConnectionFactory connectionFactory, IServiceScopeFactory scopeFactory,
            string queueName, Func<List<string>, Task> messageProcessor)
        {
            _queueName = queueName;
            _messageProcessor = messageProcessor;
            _scopeFactory = scopeFactory;
            var retryCount = 5;
            var retryDelay = 5000;
            while (retryCount > 0)
            {
                try
                {
                    _connection = connectionFactory.CreateConnectionAsync().Result;
                    _channel = _connection.CreateChannelAsync().Result;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to RabbitMQ: {ex.Message}");
                    retryCount--;
                    Thread.Sleep(retryDelay);
                }
            }

            // Asegúrate de que la cola exista
            _channel?.QueueDeclareAsync(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null).Wait();

            // Configura el temporizador para "flush" de datos
            _flushTimer = new Timer(FlushMessages!, null, TimeSpan.Zero, TimeSpan.FromSeconds(BufferIntervalSeconds));
        }

        public async void StartConsuming()
        {
            if (_channel == null)
            {
                Console.WriteLine("Error: Channel is null");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    // Añadimos el mensaje al buffer
                    lock (_messageBuffer)
                    {
                        _messageBuffer.Add(message);
                    }

                    // Acknowledge the message
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    // Opción: Agregar lógica de reintentos o registros
                }
            };

            // Asociamos el consumidor con la cola
            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false,
                consumer: consumer
            );
        }

        // Función de "flush" que procesa los mensajes del buffer
        private async void FlushMessages(object state)
        {
            List<string>? messagesToProcess = null;

            // Bloqueamos el acceso al buffer para asegurar que no haya acceso concurrente
            lock (_messageBuffer)
            {
                if (_messageBuffer.Any())
                {
                    messagesToProcess = new List<string>(_messageBuffer);
                    _messageBuffer.Clear();
                }
            }

            if (messagesToProcess != null && messagesToProcess.Any())
            {
                try
                {
                    // Procesamos los mensajes acumulados
                    await _messageProcessor(messagesToProcess);
                    Console.WriteLine($"Processed {messagesToProcess.Count} messages.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing buffered messages: {ex.Message}");
                }
            }
        }

        public async void StopConsuming()
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
            }
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }
        }
    }
}