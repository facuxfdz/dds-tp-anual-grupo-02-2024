using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations;
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
            .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Test"), ServiceLifetime.Singleton)
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddOperationsLayerMock()
            .BuildServiceProvider();

        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        using (scope)
        {
            
            var premio = new Premio
            {
                Nombre = "Premio 123",
                PuntosNecesarios = 220,
                Rubro = TipoRubro.Electronica,
                Imagen = "Imagen 123",

            };
            context.Premios.Add(premio);
            
            
            var direccion = new Direccion
            {
                Calle = "Avellaneda",
                Numero = "213",
                CodigoPostal = "234",
                Departamento = "D 14",
                Localidad = "Avellaneda",
                Piso = "6"
            };
            
            context.Direcciones.Add(direccion);

            var puntoEstrategico = new PuntoEstrategico
            {
                Direccion = direccion,
                Nombre = "Avellaneda",
                Latitud = 1243,
                Longitud = 1245,
            };
            
            context.PuntosEstrategicos.Add(puntoEstrategico);

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
            
            /*context.Viandas.Add(vianda1);
            context.Viandas.Add(vianda2);
            context.Viandas.Add(vianda3);*/

            
            
            var heladeraOrigen = new Heladera
            { 
                TemperaturaMaximaConfig = 28,
                TemperaturaMinimaConfig = -20,
                Modelo = new ModeloHeladera
                {
                    Capacidad = 10,
                    TemperaturaMinima = -20,
                    TemperaturaMaxima = 28
                },
                Estado = EstadoHeladera.Activa,
                PuntoEstrategico = puntoEstrategico
            };
            heladeraOrigen.IngresarVianda(vianda1);
            heladeraOrigen.IngresarVianda(vianda2);
            vianda1.Heladera = heladeraOrigen;
            vianda2.Heladera = heladeraOrigen;
            context.Heladeras.Add(heladeraOrigen);

            var heladeraDestino = new Heladera
            { 
                TemperaturaMaximaConfig = 28,
                TemperaturaMinimaConfig = -20,
                Modelo = new ModeloHeladera
                {
                    Capacidad = 10,
                    TemperaturaMinima = -20,
                    TemperaturaMaxima = 28
                },
                Estado = EstadoHeladera.Activa,
                PuntoEstrategico = puntoEstrategico
            };
            heladeraDestino.IngresarVianda(vianda3);
            vianda3.Heladera = heladeraDestino;
            context.Heladeras.Add(heladeraDestino);

            var registroTemperatura = new RegistroTemperatura
            {
                Date = DateTime.Now,
                Temperatura = 24
            };

            var sensorTemperatura1 = new SensorTemperatura
            {
                RegistrosTemperatura = [registroTemperatura]

            };
            
            var sensorTemperatura2 = new SensorTemperatura
            {
                RegistrosTemperatura = [registroTemperatura]

            };
            sensorTemperatura1.Heladera = heladeraOrigen;
            sensorTemperatura2.Heladera = heladeraDestino;
            heladeraOrigen.AgregarSensor(sensorTemperatura1);
            heladeraDestino.AgregarSensor(sensorTemperatura2);
            
            var donacionVianda = new DonacionVianda();
            context.FormasContribucion.Add(donacionVianda);
            
            var donacionDistribucionVianda = new DistribucionViandas
            {
                CantViandas = 1,
                FechaContribucion = DateTime.Now,
                HeladeraOrigen = heladeraOrigen,
                HeladeraDestino = heladeraDestino
                
            };
            context.FormasContribucion.Add(donacionDistribucionVianda);
           
            var personaHumana = new PersonaHumana
            {
                Nombre = "Juan Pérez",
                DocumentoIdentidad = new DocumentoIdentidad(TipoDocumento.DNI, "12345678", new DateTime(2020, 1, 1)),
            };
    
            context.Personas.Add(personaHumana);
    
           
            var colaborador = new Colaborador
            {
                Persona = personaHumana, 
                Puntos = 5000, 
            };
            colaborador.AgregarContribucion(donacionVianda);
            colaborador.AgregarContribucion(donacionDistribucionVianda);

            context.Roles.Add(colaborador);

           
            var personaVulnerable = new PersonaVulnerable
            {
                Persona = personaHumana,
                CantidadDeMenores = 0,
                FechaRegistroSistema = DateTime.Now,

            };
            
            context.Roles.Add(personaVulnerable);
            
            
            var tarjetaConsumo = new TarjetaConsumo
            {
                Responsable = colaborador,
                Codigo = "248urqe8393",
                Accesos = []

            };
            
            tarjetaConsumo.Propietario = personaVulnerable;
            personaVulnerable.Tarjeta = tarjetaConsumo;
            context.Tarjetas.Add(tarjetaConsumo);
            
            var accesoHeladera = new AccesoHeladera
            {
                
                FechaAcceso = DateTime.UtcNow,
                Heladera = heladeraOrigen,
                Tarjeta = tarjetaConsumo,
                TipoAcceso = TipoAcceso.RetiroVianda,
                Viandas = [vianda1]
                
            };
            
            //tarjetaConsumo.Accesos.Add(accesoHeladera);

            var autorazacion1 = new AutorizacionManipulacionHeladera
            {
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddDays(1),
                Heladera = heladeraOrigen

            };
            
            accesoHeladera.Autorizacion = autorazacion1;

            var tarjetaColaboracion = new TarjetaColaboracion
            {
                Accesos = [],
                Autorizaciones = [],
                Codigo = "fehui897532dsg",
                Propietario = colaborador
            };
            
            context.Tarjetas.Add(tarjetaColaboracion);
            autorazacion1.TarjetaAutorizada = tarjetaColaboracion;
            tarjetaColaboracion.AgregarAutorizacion(autorazacion1);
            
            var reporteCantidadFallasPorHeladera = new Reporte
            {
                Tipo = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA,
                FechaExpiracion = DateTime.Now,
                Cuerpo = "Reporte de prueba"
            };
        
            context.Reportes.Add(reporteCantidadFallasPorHeladera);
            
            context.SaveChanges();
        }

        
        return serviceProvider;
        
        
    }
    
}