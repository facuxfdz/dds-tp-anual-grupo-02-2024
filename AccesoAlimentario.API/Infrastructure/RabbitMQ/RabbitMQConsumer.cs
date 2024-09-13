using System.Text;
using System.Timers;
using AccesoAlimentario.API.UseCases.RegistrarDataHeladera;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Timer = System.Timers.Timer;

namespace AccesoAlimentario.API.Infrastructure.RabbitMQ;

public class RabbitMQConsumer : IDisposable
{
    private readonly string _queueName;
    private readonly IRegistrarEventoHeladera _messageProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly List<string> _messageBatch;
    private readonly object _lockObject = new();
    private readonly Timer _batchTimer;
    private const int BatchIntervalMilliseconds = 5000; // Flush batch cada 30 segundos

    public RabbitMQConsumer(
        string queueName, 
        IRegistrarEventoHeladera messageProcessor)
    {
        _queueName = queueName;
        _messageProcessor = messageProcessor;
        _messageBatch = new List<string>();

        // Configuración de la conexión RabbitMQ
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declarar la cola
        _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        // Initialize the batch timer
        _batchTimer = new Timer(BatchIntervalMilliseconds);
        _batchTimer.Elapsed += OnBatchTimerElapsed;
        _batchTimer.Start();
    }

    public void Listen()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Add message to the batch
            lock (_lockObject)
            {
                _messageBatch.Add(message);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    // This method will be triggered periodically by the timer
    private void OnBatchTimerElapsed(object sender, ElapsedEventArgs e)
    {
        List<string> batchToProcess;

        // Lock and swap the batch
        lock (_lockObject)
        {
            if (_messageBatch.Count == 0) return;  // Nothing to process
            batchToProcess = new List<string>(_messageBatch); // Copy the batch
            _messageBatch.Clear();  // Clear the original batch
        }

        // Pass the accumulated batch to the message processor
        _messageProcessor.RegistrarEvento(batchToProcess);
    }

    public void Dispose()
    {
        _batchTimer?.Dispose();
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
