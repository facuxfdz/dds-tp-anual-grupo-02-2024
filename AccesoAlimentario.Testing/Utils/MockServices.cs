using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AccesoAlimentario.Testing.Utils;

public class MockServices
{
    private ServiceProvider _serviceProvider;
    
    public MockServices()
    {
        _serviceProvider = Get();
    } 
    
    public IMediator GetMediator()
    {
        return _serviceProvider.GetRequiredService<IMediator>();
    }
    
    public IServiceScope GetScope()
    {
        // Este método crea un alcance (scope) dentro de los servicios registrados
        return _serviceProvider.CreateScope();
    }
    public static ServiceProvider Get()
    {
        var services = new ServiceCollection();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders(); // Limpia cualquier proveedor existente
            loggingBuilder.AddSerilog();    // Agrega Serilog como proveedor
        });
        var serviceProvider = services
            .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Test"))
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddOperationsLayerMock()
            .BuildServiceProvider();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
            // Crear y agregar una contribucion de prueba
            var donacionVianda = new DonacionVianda();
            context.FormasContribucion.Add(donacionVianda);
            context.SaveChanges();
    
            // Crear y agregar una persona de prueba
            var persona = new PersonaHumana
            {
                Nombre = "Juan Pérez",
                DocumentoIdentidad = new DocumentoIdentidad(TipoDocumento.DNI, 12345678, new DateTime(2020, 1, 1)),
            };
    
            context.Personas.Add(persona);
            context.SaveChanges();
    
            // Crear y agregar un colaborador de prueba
            var colaborador = new Colaborador
            {
                Persona = persona, 
                Puntos = 100, 
            };
            colaborador.AgregarContribucion(donacionVianda);

            context.Colaboradores.Add(colaborador);
            context.SaveChanges();

        }

        return serviceProvider;
    }
}