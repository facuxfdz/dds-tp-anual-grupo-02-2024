using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.API.Controllers;

public class RabbitMQBackgroundService : BackgroundService
{
    private readonly ILogger<RabbitMQBackgroundService> _logger;
    private readonly RabbitMQConsumer[] _consumers;

    public RabbitMQBackgroundService()
    {
        // Initialize consumers for different queues
        var temperaturaProcessor = new RegistroTemperaturaProcessor();
        var fraudeProcessor = new RegistroFraudeProcessor();
        _consumers = new RabbitMQConsumer[]
        {
            new RabbitMQConsumer("temperature_queue", temperaturaProcessor),
            new RabbitMQConsumer("fraud_queue", fraudeProcessor)
        };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        // Start listening on all consumers in parallel
        foreach (var consumer in _consumers)
        {
            Task.Run(() => consumer.Listen(), stoppingToken);
        }

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        // Dispose each consumer
        foreach (var consumer in _consumers)
        {
            consumer.Dispose();
        }

        base.Dispose();
    }
}