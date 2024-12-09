using System.Text;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
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
    public IMediator GetMediator()
    {
        return GetServiceProvider().GetRequiredService<IMediator>();
    }

    public IServiceScope GetScope()
    {
        // Este método crea un alcance (scope) dentro de los servicios registrados
        return GetServiceProvider().CreateScope();
    }

    public static string CrearArchivoCsvColaboradores()
    {
        var csvContent = new StringBuilder();
        csvContent.AppendLine("TipoDoc,Documento,Nombre,Apellido,Mail,FechaColaboracion,FormaColaboracion,Cantidad");

        csvContent.AppendLine("DNI,12345678,Juan,Pérez,laripallenzona@gmail.com,22/03/2024,DONACION_VIANDAS,2");
        csvContent.AppendLine("DNI,87654321,usuario2,password2,laripallenzona@gmail.com,22/03/2024,DONACION_VIANDAS,1");

        var csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());
        return Convert.ToBase64String(csvBytes);
    }

    private ServiceProvider GetServiceProvider()
    {
        return Get();
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
            loggingBuilder.AddSerilog(); // Agrega Serilog como proveedor
        });

        var serviceProvider = services
            .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase($"Test-{Guid.NewGuid()}"), ServiceLifetime.Singleton)
            .AddSingleton<IUnitOfWork, UnitOfWork>()
            .AddOperationsLayerMock()
            .BuildServiceProvider();

        SeedDatabase(serviceProvider);

        return serviceProvider;
    }

    private static void SeedDatabase(ServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        using (scope)
        {
            var premio = new Premio
            {
                Id = Guid.Parse("4f1cf41b-c3b3-4c0b-8a0f-48d4bf82b4ec"),
                Nombre = "Premio 123",
                PuntosNecesarios = 220,
                Rubro = TipoRubro.Electronica,
                Imagen = "Imagen 123",
            };
            context.Premios.Add(premio);

            var heladeraOrigen = new Heladera
            {
                Id = Guid.Parse("77488f31-6fb2-48a0-9440-e1b8d85be951"),
                TemperaturaMaximaConfig = 28,
                TemperaturaMinimaConfig = -20,
                Modelo = new ModeloHeladera
                {
                    Id = Guid.Parse("794fd59e-9913-4a2f-aa3c-797c69bc9488"),
                    Capacidad = 10,
                    TemperaturaMinima = -20,
                    TemperaturaMaxima = 28
                },
                Estado = EstadoHeladera.Activa,
                PuntoEstrategico = new PuntoEstrategico
                {
                    Id = Guid.Parse("e1f709f9-04e0-438d-a7f9-25f7e734a461"),
                    Direccion = new Direccion
                    {
                        Id = Guid.Parse("1d9806c7-de36-4b47-9eff-772d806f0bb8"),
                        Calle = "Avellaneda",
                        Numero = "213",
                        CodigoPostal = "234",
                        Departamento = "D 14",
                        Localidad = "Avellaneda",
                        Piso = "6"
                    },
                    Nombre = "Avellaneda",
                    Latitud = 1243,
                    Longitud = 1245,
                },
                Incidentes = [new FallaTecnica
                {
                    Id = Guid.Parse("b5eaa261-56a2-48c9-bef6-5e1cbe87d128"),
                    Fecha = DateTime.Now,
                    Descripcion = "Falla técnica 1"
                }],
                Viandas =
                [
                    new Vianda
                    {
                        Id = Guid.Parse("84131157-a354-4177-b7fe-ed1161ff7e1f"),
                        Comida = "comida",
                        FechaCaducidad = DateTime.Now,
                        Estado = EstadoVianda.Disponible
                    },
                    new Vianda
                    {
                        Id = Guid.Parse("b94ad052-e8ed-4ce9-828b-899f89353a56"),
                        Comida = "comida",
                        FechaCaducidad = DateTime.Now,
                        Estado = EstadoVianda.Disponible
                    }
                ],
                Sensores =
                [
                    new SensorTemperatura
                    {
                        Id = Guid.Parse("1acb323f-833a-4775-9a9c-2f64951fcb94"),
                        RegistrosTemperatura =
                        [
                            new RegistroTemperatura
                            {
                                Id = Guid.Parse("d4dd23be-5f65-4b1e-8f31-930f9db73bf4"),
                                Date = DateTime.Now,
                                Temperatura = 24
                            }
                        ]
                    }
                ]
            };
            
            context.Direcciones.Add(heladeraOrigen.PuntoEstrategico.Direccion);
            context.PuntosEstrategicos.Add(heladeraOrigen.PuntoEstrategico);
            context.Incidentes.AddRange(heladeraOrigen.Incidentes);
            context.Viandas.AddRange(heladeraOrigen.Viandas);
            context.Sensores.AddRange(heladeraOrigen.Sensores);
            context.RegistrosTemperatura.AddRange(heladeraOrigen.Sensores.OfType<SensorTemperatura>()
                .SelectMany(s => s.RegistrosTemperatura));
            context.Heladeras.Add(heladeraOrigen);

            var heladeraDestino = new Heladera
            {
                Id = Guid.Parse("aaefe741-7b9a-4d4f-b176-d8af9d411ca4"),
                TemperaturaMaximaConfig = 28,
                TemperaturaMinimaConfig = -20,
                Modelo = new ModeloHeladera
                {
                    Id = Guid.Parse("ea6ed810-9d2e-4153-9165-a1301aabd41f"),
                    Capacidad = 10,
                    TemperaturaMinima = -20,
                    TemperaturaMaxima = 28
                },
                Estado = EstadoHeladera.Activa,
                PuntoEstrategico = new PuntoEstrategico
                {
                    Id = Guid.Parse("a42cfcc5-73ac-4610-9a2c-17163c586753"),
                    Direccion = new Direccion
                    {
                        Id = Guid.Parse("366a5ec1-f19e-4bb9-86f5-fd6e150f8975"),
                        Calle = "Avellaneda",
                        Numero = "213",
                        CodigoPostal = "234",
                        Departamento = "D 14",
                        Localidad = "Avellaneda",
                        Piso = "6"
                    },
                    Nombre = "Avellaneda",
                    Latitud = 1243,
                    Longitud = 1245,
                },
                Incidentes = [new FallaTecnica
                {
                    Id = Guid.Parse("a3ba343c-cb66-4346-8018-55d1d8362838"),
                    Fecha = DateTime.Now,
                    Descripcion = "Falla técnica 2"
                }],
                Viandas =
                [
                    new Vianda
                    {
                        Id = Guid.Parse("5df4dc3a-aa5e-4724-ae5a-8992da4fcff0"),
                        Comida = "comida",
                        FechaCaducidad = DateTime.Now,
                        Estado = EstadoVianda.Disponible
                    }
                ],
                Sensores =
                [
                    new SensorTemperatura
                    {
                        Id = Guid.Parse("bc547c7f-563b-46d3-beb3-61d21266a04d"),
                        RegistrosTemperatura =
                        [
                            new RegistroTemperatura
                            {
                                Id = Guid.Parse("1f7a95dc-9e0c-4800-93cb-e27a7de4753e"),
                                Date = DateTime.Now,
                                Temperatura = 24
                            }
                        ]
                    }
                ]
            };
            context.Direcciones.Add(heladeraDestino.PuntoEstrategico.Direccion);
            context.PuntosEstrategicos.Add(heladeraDestino.PuntoEstrategico);
            context.Incidentes.AddRange(heladeraDestino.Incidentes);
            context.Viandas.AddRange(heladeraDestino.Viandas);
            context.Sensores.AddRange(heladeraDestino.Sensores);
            context.RegistrosTemperatura.AddRange(heladeraDestino.Sensores.OfType<SensorTemperatura>()
                .SelectMany(s => s.RegistrosTemperatura));
            context.Heladeras.Add(heladeraDestino);
            
            var colaborador = new Colaborador
            {
                Id = Guid.Parse("649a0daa-a7a9-4c84-b419-5bc29be413f4"),
                Persona = new PersonaHumana
                {
                    Id = Guid.Parse("6c5f5397-996f-4af7-8537-8b39fc1a8aa0"),
                    Nombre = "Juan Pérez",
                    DocumentoIdentidad = new DocumentoIdentidad
                    {
                        Id = Guid.Parse("599de0dc-f808-408b-bf7c-8c77980ff01f"),
                        TipoDocumento = TipoDocumento.DNI,
                        NroDocumento = "12345678",
                        FechaNacimiento = new DateTime(2020, 1, 1)
                    },
                },
                Puntos = 5000,
                ContribucionesRealizadas = [
                    new DonacionVianda
                    {
                        Id = Guid.Parse("24be7304-ae4c-4a00-a212-f94924cc7f9b"),
                        FechaContribucion = DateTime.Now,
                    },
                    new DistribucionViandas
                    {
                        Id = Guid.Parse("d8a2ca77-3dcd-4023-bf5e-57fda81006c3"),
                        CantViandas = 1,
                        FechaContribucion = DateTime.Now,
                        HeladeraOrigen = heladeraOrigen,
                        HeladeraDestino = heladeraDestino
                    }
                ]
            };

            context.DocumentosIdentidad.Add(colaborador.Persona.DocumentoIdentidad);
            context.Personas.Add(colaborador.Persona);
            context.FormasContribucion.AddRange(colaborador.ContribucionesRealizadas);
            context.Roles.Add(colaborador);

            var tecnico = new Tecnico
            {
                Id = Guid.Parse("d304e31e-086b-4080-b20e-f5df7d2a9766"),
                AreaCobertura = new AreaCobertura
                {
                    Id = Guid.Parse("c65b1276-e07f-427f-8359-1bd221047a59"),
                    Latitud = 0,
                    Longitud = 0,
                    Radio = 2
                },
                Persona = new PersonaHumana
                {
                    Id = Guid.Parse("24f6f811-9372-4466-8df4-7f4fe92159d8"),
                    Nombre = "Pedron Manuel",
                    DocumentoIdentidad =
                        new DocumentoIdentidad
                        {
                            Id = Guid.Parse("d0fe28fb-d13b-4335-9d1a-7cd52828555f"),
                            TipoDocumento = TipoDocumento.DNI,
                            NroDocumento = "12345678",
                            FechaNacimiento = new DateTime(2020, 1, 1)
                        },
                }
            };
            context.AreasCobertura.Add(tecnico.AreaCobertura);
            context.DocumentosIdentidad.Add(tecnico.Persona.DocumentoIdentidad);
            context.Personas.Add(tecnico.Persona);
            context.Roles.Add(tecnico);

            var personaVulnerable = new PersonaVulnerable
            {
                Id = Guid.Parse("61c4dffa-56d5-41d0-81b6-79814ee0ee94"),
                Persona = new PersonaHumana
                {
                    Id = Guid.Parse("12c86a27-cddf-472b-bd7d-59d6ae35b430"),
                    Nombre = "Pedron Manuel",
                    DocumentoIdentidad =
                        new DocumentoIdentidad
                        {
                            Id = Guid.Parse("7e08bccb-35c9-47ed-be93-97b8ad7e43d4"),
                            TipoDocumento = TipoDocumento.DNI,
                            NroDocumento = "12345678",
                            FechaNacimiento = new DateTime(2020, 1, 1)
                        },
                },
                CantidadDeMenores = 0,
                FechaRegistroSistema = DateTime.Now,
            };
            context.DocumentosIdentidad.Add(personaVulnerable.Persona.DocumentoIdentidad);
            context.Personas.Add(personaVulnerable.Persona);
            context.Roles.Add(personaVulnerable);


            var tarjetaConsumo = new TarjetaConsumo
            {
                Id = Guid.Parse("d588c2d9-c000-4420-8334-e56351384315"),
                Responsable = colaborador,
                Codigo = "248urqe8393",
                Accesos = []
            };

            tarjetaConsumo.Propietario = personaVulnerable;
            personaVulnerable.Tarjeta = tarjetaConsumo;
            context.Tarjetas.Add(tarjetaConsumo);

            var accesoHeladera = new AccesoHeladera
            {
                Id = Guid.Parse("4a0a57fe-3403-443c-842f-b48dad15a1d6"),
                FechaAcceso = DateTime.UtcNow,
                Heladera = heladeraOrigen,
                Tarjeta = tarjetaConsumo,
                TipoAcceso = TipoAcceso.RetiroVianda,
                Viandas = [
                    heladeraOrigen.Viandas[0]
                ]
            };
            context.AccesosHeladera.Add(accesoHeladera);
            
            var autorazacion1 = new AutorizacionManipulacionHeladera
            {
                Id = Guid.Parse("a812ffdd-32a7-4590-b6e5-752249af1c05"),
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddDays(1),
                Heladera = heladeraOrigen
            };

            accesoHeladera.Autorizacion = autorazacion1;
            context.AutorizacionesManipulacionHeladera.Add(autorazacion1);

            var tarjetaColaboracion = new TarjetaColaboracion
            {
                Id = Guid.Parse("2fec646e-65dd-4dcf-9b56-0cb1d8bf454d"),
                Accesos = [],
                Autorizaciones = [],
                Codigo = "fehui897532dsg",
                Propietario = colaborador
            };

            context.Tarjetas.Add(tarjetaColaboracion);
            autorazacion1.TarjetaAutorizada = tarjetaColaboracion;
            tarjetaColaboracion.AgregarAutorizacion(autorazacion1);

            var reporteCantidadViandasPorColaborador = new Reporte
            {
                Id = Guid.Parse("2cd69530-5c3c-4c08-ae91-486909f11e5c"),
                Tipo = TipoReporte.CANTIDAD_VIANDAS_POR_COLABORADOR,
                FechaExpiracion = DateTime.UtcNow.AddDays(-1),
                Cuerpo = "Reporte de prueba"
            };

            context.Reportes.Add(reporteCantidadViandasPorColaborador);

            context.SaveChanges();
        }
    }
}