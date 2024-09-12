using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Servicios;
using AccesoAlimentario.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Testing;

/*
    - Testear que los puntos son generados correctamente (testear que el numerito sea el esperado). Estos puntos estan en el colaborador
*/
public class TestPuntosColaboracion
{
    private PersonaHumana unaPersonaHumana;
    private PersonaHumana otraPersonaHumana;
    private Colaborador unColaboradorSinPuntos;
    private Colaborador unColaboradorConPuntos;
    private Vianda unaVianda;
    private Premio unPremio;
    private Heladera unaHeladera;
    private Heladera otraHeladera;
    private ColaboracionesServicio colaboracionesServicio;
    private ColaboradoresServicio colaboradoresServicio;
    private PersonasServicio personasServicio;
    private TarjetaConsumo unaTarjetaConsumo;
    private PersonaJuridica unaPersonaJuridica;
    private Colaborador unColaboradorJuridico;


    [SetUp]
    public void Setup()
    {
        unaPersonaHumana = new PersonaHumana("Pepe", "Gonzalez", null, null, null, SexoDocumento.Femenino);
        unaPersonaJuridica = new PersonaJuridica("Pepe", "Gonzalez", TipoJuridico.Gubernamental, "", null, null, null);
        otraPersonaHumana = new PersonaHumana("Pepa", "Lopez", null, null, null, SexoDocumento.Femenino);
        unColaboradorSinPuntos = new Colaborador(unaPersonaHumana, []);
        unColaboradorJuridico = new Colaborador(unaPersonaJuridica, []);
        unColaboradorConPuntos = new Colaborador(unaPersonaHumana, []);
        unColaboradorConPuntos.AgregarPuntos(250);

        unPremio = new Premio("jamon", 200, "", TipoRubro.Gastronomia);
        unaVianda = new Vianda("milanesa", DateTime.Now, DateTime.Now, unColaboradorSinPuntos, unaHeladera, 100, 100, EstadoVianda.Disponible, null);
        unaHeladera = new Heladera();
        otraHeladera = new Heladera();
        unaHeladera.IngresarVianda(unaVianda);
        unaTarjetaConsumo = new TarjetaConsumo(unColaboradorSinPuntos, "123", null);

        var options = new DbContextOptionsBuilder<AppDbContext>(options: new DbContextOptions<AppDbContext>())
            .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
            .Options;
        var dbcontext = new AppDbContext(options);
        var unitOfWork = new UnitOfWork(dbcontext);
        colaboradoresServicio = new ColaboradoresServicio(unitOfWork, new PersonasServicio(unitOfWork));
        colaboracionesServicio = new ColaboracionesServicio(unitOfWork, colaboradoresServicio);
        
        unitOfWork.ColaboradorRepository.Insert(unColaboradorSinPuntos);
    }

    [Test]
    public void CrearAdministracionHeladera_PuntosGenerados_CeroPuntos()
    {
        colaboracionesServicio.CrearAdministracionHeladera(unColaboradorJuridico, unaHeladera, null);
        Assert.AreEqual(0, unColaboradorSinPuntos.ObtenerPuntos());
    }

    [Test]
    public void CrearDistribucionViandas_PuntosGenerados_UnoPorVianda()
    {
        colaboracionesServicio.CrearDistribucionViandas(unColaboradorSinPuntos, unaHeladera, otraHeladera, 1,
            MotivoDistribucion.FaltaDeViandas, null);
        Assert.AreEqual(1, unColaboradorSinPuntos.ObtenerPuntos());
    }

    [Test]
    public void CrearRegistroPersonaVulnerable_PuntosGenerados_DosPorTarjetaRepartida()
    {
        colaboracionesServicio.CrearRegistroPersonaVulnerable(unColaboradorSinPuntos, otraPersonaHumana, 1, unaTarjetaConsumo, null);
        Assert.AreEqual(2, unColaboradorSinPuntos.ObtenerPuntos());
    }

    [Test]
    public void CrearDonacionMonetaria_PuntosGenerados_LaMitadDeLoDonado()
    {
        colaboracionesServicio.CrearDonacionMonetaria(unColaboradorSinPuntos, 100, 1, null);
        Assert.AreEqual(50, unColaboradorSinPuntos.ObtenerPuntos());
    }

    [Test]
    public void CrearDonacionVianda_PuntosGenerados_UnoYMedioPorVianda()
    {
        colaboracionesServicio.CrearDonacionVianda(unColaboradorSinPuntos, unaHeladera, unaVianda, null);
        Assert.AreEqual(15, unColaboradorSinPuntos.ObtenerPuntos());
    }
    
    [Test]
    public void CrearOfertaPremio_PuntosGenerados_CeroPuntos()
    {
        colaboracionesServicio.CrearOfertaPremio(unColaboradorJuridico, unPremio, null);
        Assert.AreEqual(0, unColaboradorSinPuntos.ObtenerPuntos());
    }
}