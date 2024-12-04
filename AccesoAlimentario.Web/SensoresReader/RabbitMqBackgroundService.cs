using AccesoAlimentario.Web.SecretRetrieve;
using AccesoAlimentario.Web.SensoresReader;
using AccesoAlimentario.Web.SensoresReader.Processors;
using MediatR;
using RabbitMQ.Client;

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
        // if development retrieve rabbitconfig from environment variables
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var rabbitConfig = new ConnectionFactory();
        if (env == "Development")
        {
            rabbitConfig.HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
            rabbitConfig.UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest";
            rabbitConfig.Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
        }
        else
        {
            SecretRetrieve secretRetrieve = new SecretRetrieve();
            var rabbitSecret = secretRetrieve.GetSecretAs<RabbitMQSecret>("rabbitmq");
            if (rabbitSecret == null)
            {
                throw new Exception("RabbitMQ secret not found");
            }
            rabbitConfig.HostName = rabbitSecret.HostName;
            rabbitConfig.UserName = rabbitSecret.UserName;
            rabbitConfig.Password = rabbitSecret.Password;
        }
        _consumers = new List<RabbitMQConsumer>
        {
            new RabbitMQConsumer(rabbitConfig, scopeFactory, "temperatura", tempProcessor.ProcessMessageBuffered)
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