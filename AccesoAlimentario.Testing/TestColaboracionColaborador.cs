// using AccesoAlimentario.Core.DAL;
// using AccesoAlimentario.Core.Entities.Contribuciones;
// using AccesoAlimentario.Core.Entities.Heladeras;
// using AccesoAlimentario.Core.Entities.Personas;
// using AccesoAlimentario.Core.Entities.Premios;
// using AccesoAlimentario.Core.Entities.Roles;
// using AccesoAlimentario.Core.Entities.Tarjetas;
// using Microsoft.EntityFrameworkCore;
//
// namespace AccesoAlimentario.Testing;
//
// /*
//     - Testear que los puntos son generados correctamente (testear que el numerito sea el esperado). Estos puntos estan en el colaborador
// */
// public class TestColaboracionColaborador
// {
//     private PersonaHumana unaPersonaHumana;
//     private PersonaHumana otraPersonaHumana;
//     private Colaborador unColaboradorSinPuntos;
//     private Colaborador unColaboradorConPuntos;
//     private Vianda unaVianda;
//     private Premio unPremio;
//     private Heladera unaHeladera;
//     private Heladera otraHeladera;
//     private ColaboracionesServicio colaboracionesServicio;
//     private ColaboradoresServicio colaboradoresServicio;
//     private PersonasServicio personasServicio;
//     private TarjetaConsumo unaTarjetaConsumo;
//     private PersonaJuridica unaPersonaJuridica;
//     private Colaborador unColaboradorJuridico;
//
//
//     [SetUp]
//     public void Setup()
//     {
//         unaPersonaHumana = new PersonaHumana("Pepe", "Gonzalez", null, null, null, SexoDocumento.Femenino);
//         unaPersonaJuridica = new PersonaJuridica("Pepe", "Gonzalez", TipoJuridico.Gubernamental, "", null, null, null);
//         otraPersonaHumana = new PersonaHumana("Pepa", "Lopez", null, null, null, SexoDocumento.Femenino);
//         unColaboradorSinPuntos = new Colaborador(unaPersonaHumana, []);
//         unColaboradorJuridico = new Colaborador(unaPersonaJuridica, []);
//         unColaboradorConPuntos = new Colaborador(unaPersonaHumana, []);
//         unColaboradorConPuntos.AgregarPuntos(250);
//
//         unPremio = new Premio("jamon", 200, "", TipoRubro.Gastronomia);
//         unaVianda = new Vianda("milanesa", DateTime.Now, DateTime.Now, unColaboradorSinPuntos, unaHeladera, 100, 100, EstadoVianda.Disponible, null);
//         unaHeladera = new Heladera();
//         otraHeladera = new Heladera();
//         unaHeladera.IngresarVianda(unaVianda);
//         unaTarjetaConsumo = new TarjetaConsumo(unColaboradorSinPuntos, "123", null);
//
//         var options = new DbContextOptionsBuilder<AppDbContext>(options: new DbContextOptions<AppDbContext>())
//             .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
//             .Options;
//         var dbcontext = new AppDbContext(options);
//         var unitOfWork = new UnitOfWork(dbcontext);
//         colaboradoresServicio = new ColaboradoresServicio(unitOfWork, new PersonasServicio(unitOfWork));
//         colaboracionesServicio = new ColaboracionesServicio(unitOfWork, colaboradoresServicio);
//         
//         unitOfWork.ColaboradorRepository.Insert(unColaboradorJuridico);
//         unitOfWork.ColaboradorRepository.Insert(unColaboradorSinPuntos);
//     }
//
//     [Test]
//     public void CrearAdministracionHeladera_ColaboradorJuridico_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearAdministracionHeladera(unColaboradorJuridico, unaHeladera, null));
//     }
//     
//     [Test]
//     public void CrearAdministracionHeladera_ColaboradorHumano_NoPuedeColaborar()
//     {
//         Assert.Throws<Exception>(() => colaboracionesServicio.CrearAdministracionHeladera(unColaboradorSinPuntos, unaHeladera, null));
//     }
//
//     [Test]
//     public void CrearDistribucionViandas_ColaboradorHumano_PuedeColaborar()
//     {
//             Assert.DoesNotThrow(() => colaboracionesServicio.CrearDistribucionViandas(unColaboradorSinPuntos, unaHeladera, otraHeladera, 1, 
//             MotivoDistribucion.FaltaDeViandas, null));
//     }
//     
//     [Test]
//     public void CrearDistribucionViandas_ColaboradorJuridico_NoPuedeColaborar()
//     {
//         Assert.Throws<Exception>(() => colaboracionesServicio.CrearDistribucionViandas(unColaboradorJuridico, unaHeladera, otraHeladera, 1, 
//             MotivoDistribucion.FaltaDeViandas, null));
//     }
//
//     [Test]
//     public void CrearRegistroPersonaVulnerable_ColaboradorJuridico_NoPuedeColaborar()
//     {
//         Assert.Throws<Exception>(() => colaboracionesServicio.CrearRegistroPersonaVulnerable(unColaboradorJuridico, otraPersonaHumana, 1, unaTarjetaConsumo, null));
//     }
//     
//     [Test]
//     public void CrearRegistroPersonaVulnerable_ColaboradorHumano_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearRegistroPersonaVulnerable(unColaboradorSinPuntos, otraPersonaHumana, 1, unaTarjetaConsumo, null));
//     }
//
//     [Test]
//     public void CrearDonacionMonetaria_ColaboradorHumano_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearDonacionMonetaria(unColaboradorSinPuntos, 100, 1, null));
//     }
//     
//     [Test]
//     public void CrearDonacionMonetaria_ColaboradorJuridico_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearDonacionMonetaria(unColaboradorJuridico, 100, 1, null));
//     }
//
//     [Test]
//     public void CrearDonacionVianda_ColaboradorHumano_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearDonacionVianda(unColaboradorSinPuntos, unaHeladera, unaVianda, null));
//     }
//     
//     [Test]
//     public void CrearDonacionVianda_ColaboradorJuridico_NoPuedeColaborar()
//     {
//         Assert.Throws<Exception>(() => colaboracionesServicio.CrearDonacionVianda(unColaboradorJuridico, unaHeladera, unaVianda, null));
//     }
//     
//     [Test]
//     public void CrearOfertaPremio_ColaboradorJuridico_PuedeColaborar()
//     {
//         Assert.DoesNotThrow(() => colaboracionesServicio.CrearOfertaPremio(unColaboradorJuridico, unPremio, null));
//     }
//         
//     [Test]
//     public void CrearOfertaPremio_ColaboradorHumano_NoPuedeColaborar()
//     {
//         Assert.Throws<Exception>(() => colaboracionesServicio.CrearOfertaPremio(unColaboradorSinPuntos, unPremio, null));
//     }
// }