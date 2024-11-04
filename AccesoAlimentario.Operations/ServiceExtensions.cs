using System.Reflection;
using AccesoAlimentario.Operations.Reportes;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Operations;

public static class ServiceExtensions
{
    public static void AddOperationsLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHostedService<CrearReportesService>();
    }
}