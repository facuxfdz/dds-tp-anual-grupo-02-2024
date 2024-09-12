using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

namespace AccesoAlimentario.API.Infrastructure.RabbitMQ;

public class RabbitMQBackgroundService : BackgroundService
{
    private readonly ILogger<RabbitMQBackgroundService> _logger;
    private readonly RabbitMQConsumer[] _consumers;

    public RabbitMQBackgroundService(IServiceScopeFactory factory)
    {
        // Initialize consumers for different queues
        // var registrarFraudeHeladera = factory.CreateScope().ServiceProvider.GetRequiredService<RegistrarFraudeHeladera>();
        // var registrarTemperaturaHeladera = factory.CreateScope().ServiceProvider.GetRequiredService<RegistrarTemperaturaHeladera>();
        var fraudeHeladeraRepository = factory.CreateScope().ServiceProvider.GetRequiredService<GenericRepository<RegistroFraude>>();
        var temperaturaHeladeraRepository = factory.CreateScope().ServiceProvider.GetRequiredService<GenericRepository<RegistroTemperatura>>();
        var registrarFraude = new RegistrarFraudeHeladera(fraudeHeladeraRepository);
        var registrarTemperatura = new RegistrarTemperaturaHeladera(temperaturaHeladeraRepository);
        _consumers = new RabbitMQConsumer[]
        {
            new("temperature_queue", registrarTemperatura),
            new("fraud_queue", registrarFraude)
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