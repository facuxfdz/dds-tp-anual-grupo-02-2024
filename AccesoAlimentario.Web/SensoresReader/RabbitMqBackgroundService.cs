using AccesoAlimentario.Web.SensoresReader;
using AccesoAlimentario.Web.SensoresReader.Processors;
using MediatR;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly IEnumerable<RabbitMQConsumer> _consumers;
    private readonly IServiceScope _scope;
    
    public RabbitMqBackgroundService(IServiceScopeFactory scopeFactory)
    {
        Console.WriteLine("RabbitMqBackgroundService constructor");
        _scope = scopeFactory.CreateScope();
        var sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        var tempProcessor = new TemperaturaProcessor(sender);  
        _consumers = new List<RabbitMQConsumer>
        {
            new RabbitMQConsumer(scopeFactory, "temperatura", tempProcessor.ProcessMessageBuffered)
        };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var consumer in _consumers)
        {
            consumer.StartConsuming();
        }
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var consumer in _consumers)
        {
            consumer.StopConsuming();
        }
        return base.StopAsync(cancellationToken);
    }
}