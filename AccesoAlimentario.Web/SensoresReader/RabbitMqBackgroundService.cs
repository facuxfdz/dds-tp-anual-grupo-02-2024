using AccesoAlimentario.Web.ParametersRetrieve;
using AccesoAlimentario.Web.SecretRetrieve;
using AccesoAlimentario.Web.SensoresReader.Processors;
using MediatR;
using RabbitMQ.Client;

namespace AccesoAlimentario.Web.SensoresReader;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly IEnumerable<RabbitMqConsumer> _consumers;
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
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
            var secretName = new SSMParameterRetriever
            {
                Region = Environment.GetEnvironmentVariable("AWS_REGION") ?? "us-east-1"
            }.GetString("/accesoalimentario/rabbitmq/secret_name");
            SecretRetrieve.SecretRetrieve secretRetrieve = new SecretRetrieve.SecretRetrieve();
            var rabbitSecret = secretRetrieve.GetSecretAs<RabbitMQSecret>(secretName);
            if (rabbitSecret == null)
            {
                throw new Exception("RabbitMQ secret not found");
            }
            rabbitConfig.HostName = rabbitSecret.host;
            rabbitConfig.UserName = rabbitSecret.username;
            rabbitConfig.Password = rabbitSecret.password;
        }
        Console.WriteLine($"RabbitMQ Host: {rabbitConfig.HostName}");
        _consumers = new List<RabbitMqConsumer>
        {
            new RabbitMqConsumer(rabbitConfig, scopeFactory, "temperatura", tempProcessor.ProcessMessageBuffered)
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