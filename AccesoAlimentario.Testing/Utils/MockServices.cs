using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
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

        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        using (scope)
        {
            var vianda1 = new Vianda
            {
                Comida = "comida",
                FechaCaducidad = DateTime.Now,
                Estado = EstadoVianda.Disponible
            };
            var vianda2 = new Vianda{
                Comida = "comida",
                FechaCaducidad = DateTime.Now,
                Estado = EstadoVianda.Disponible
            };
            
            var vianda3 = new Vianda{
                Comida = "comida",
                FechaCaducidad = DateTime.Now,
                Estado = EstadoVianda.Disponible
            };
            
            context.Viandas.Add(vianda1);
            context.Viandas.Add(vianda2);
            context.Viandas.Add(vianda3);
            context.SaveChanges();

            
            var heladeraOrigen = new Heladera
            { 
                Modelo = new ModeloHeladera
                {
                    Capacidad = 10
                },
                Viandas = [vianda1, vianda2, vianda3],
                Estado = EstadoHeladera.Activa
            };
            context.Heladeras.Add(heladeraOrigen);
            context.SaveChanges();

            var heladeraDestino = new Heladera
            { 
                Modelo = new ModeloHeladera
                {
                    Capacidad = 10
                },
                Viandas = [vianda1, vianda2, vianda3],
                Estado = EstadoHeladera.Activa
            };
            context.Heladeras.Add(heladeraDestino);
            context.SaveChanges();
            
            // Crear y agregar una contribucion de prueba
            var donacionVianda = new DonacionVianda();
            context.FormasContribucion.Add(donacionVianda);
            context.SaveChanges();
            
            var donacionDistribucionVianda = new DistribucionViandas
            {
                CantViandas = 1,
                FechaContribucion = Convert.ToDateTime("20/10/2020"),
                HeladeraOrigen = heladeraOrigen,
                HeladeraDestino = heladeraDestino
                
            };
            context.FormasContribucion.Add(donacionDistribucionVianda);
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
            colaborador.AgregarContribucion(donacionDistribucionVianda);

            context.Colaboradores.Add(colaborador);
            context.SaveChanges();
           
            
            heladeraOrigen.IngresarVianda(vianda1);
            heladeraOrigen.IngresarVianda(vianda2);
            heladeraOrigen.IngresarVianda(vianda3);
            context.SaveChanges(); // Asegura que los cambios se persistan

            heladeraDestino.IngresarVianda(vianda1);
            heladeraDestino.IngresarVianda(vianda2);
            heladeraOrigen.IngresarVianda(vianda3);
            context.SaveChanges(); // Asegura que los cambios se persistan
            
        }

        return serviceProvider;
        
        
    }
    
    public static List<Heladera> ObtenerHeladeras(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return context.Heladeras.ToList(); // Retorna todas las heladeras en la base de datos
    }
    
    
}