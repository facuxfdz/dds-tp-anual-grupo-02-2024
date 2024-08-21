using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AccesoAlimentario.API.Controllers;

public class RabbitMQConsumer
{
    private readonly string _queueName;
    // private readonly ILogger<RabbitMQConsumer> _logger;
    private readonly IHeladeraMessageProcessor _messageProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQConsumer(
        string queueName, 
        IHeladeraMessageProcessor messageProcessor)
    {
        _queueName = queueName;
        // _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<RabbitMQConsumer>();
        _messageProcessor = messageProcessor;
        // Configuración de la conexión RabbitMQ
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        // Declarar la cola
        _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Listen()
    {
        // Crear un consumidor de eventos
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            // _logger.LogInformation($"Received message from {_queueName}: {message}");

            // Procesar el mensaje (aquí puedes agregar la lógica de procesamiento)
            _messageProcessor.ProcessMessage(message);
        };

        // Iniciar consumo de la cola
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }
    

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}