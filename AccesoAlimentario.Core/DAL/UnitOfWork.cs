using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.DAL;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private BaseRepository<Persona> _personaRepository = null!;
    private BaseRepository<PersonaHumana> _personaHumanaRepository = null!;
    private BaseRepository<PersonaJuridica> _personaJuridicaRepository = null!;
    private BaseRepository<MedioContacto> _medioContactoRepository = null!;
    private BaseRepository<Telefono> _telefonoRepository = null!;
    private BaseRepository<Email> _emailRepository = null!;
    private BaseRepository<Whatsapp> _whatsappRepository = null!;
    private BaseRepository<DocumentoIdentidad> _documentoIdentidadRepository = null!;
    private BaseRepository<Direccion> _direccionRepository = null!;
    private BaseRepository<Rol> _rolRepository = null!;
    private BaseRepository<UsuarioSistema> _usuarioSistemaRepository = null!;
    private BaseRepository<PersonaVulnerable> _personaVulnerableRepository = null!;
    private BaseRepository<Tecnico> _tecnicoRepository = null!;
    private BaseRepository<Colaborador> _colaboradorRepository = null!;
    private BaseRepository<AreaCobertura> _areaCoberturaRepository = null!;
    private BaseRepository<Tarjeta> _tarjetaRepository = null!;
    private BaseRepository<TarjetaConsumo> _tarjetaConsumoRepository = null!;
    private BaseRepository<TarjetaColaboracion> _tarjetaColaboracionRepository = null!;
    private BaseRepository<FormaContribucion> _formaContribucionRepository = null!;
    private BaseRepository<AdministracionHeladera> _administracionHeladeraRepository = null!;
    private BaseRepository<DistribucionViandas> _distribucionViandasRepository = null!;
    private BaseRepository<RegistroPersonaVulnerable> _registroPersonaVulnerableRepository = null!;
    private BaseRepository<DonacionMonetaria> _donacionMonetariaRepository = null!;
    private BaseRepository<DonacionVianda> _donacionViandaRepository = null!;
    private BaseRepository<OfertaPremio> _ofertaPremioRepository = null!;
    private BaseRepository<Heladera> _heladeraRepository = null!;
    private BaseRepository<Vianda> _viandaRepository = null!;
    private BaseRepository<ViandaEstandar> _viandaEstandarRepository = null!;
    private BaseRepository<PuntoEstrategico> _puntoEstrategicoRepository = null!;
    private BaseRepository<ModeloHeladera> _modeloHeladeraRepository = null!;
    private BaseRepository<Sensor> _sensorRepository = null!;
    private BaseRepository<SensorMovimiento> _sensorMovimientoRepository = null!;
    private BaseRepository<SensorTemperatura> _sensorTemperaturaRepository = null!;
    private BaseRepository<RegistroMovimiento> _registroMovimientoRepository = null!;
    private BaseRepository<RegistroTemperatura> _registroTemperaturaRepository = null!;
    private BaseRepository<Incidente> _incidenteRepository = null!;
    private BaseRepository<VisitaTecnica> _visitaTecnicaRepository = null!;
    private BaseRepository<Premio> _premioRepository = null!;
    private BaseRepository<Notificacion> _notificacionRepository = null!;
    private BaseRepository<Suscripcion> _suscripcionRepository = null!;
    private BaseRepository<SuscripcionFaltanteViandas> _suscripcionFaltanteViandasRepository = null!;
    private BaseRepository<SuscripcionExcedenteViandas> _suscripcionExcedenteViandasRepository = null!;
    private BaseRepository<SuscripcionIncidenteHeladera> _suscripcionIncidenteHeladeraRepository = null!;
    private BaseRepository<AccesoHeladera> _accesoHeladeraRepository = null!;
    private BaseRepository<AutorizacionManipulacionHeladera> _autorizacionManipulacionHeladeraRepository = null!;

    /* ------------------------ */
    public IBaseRepository<Persona> PersonaRepository => _personaRepository ??= new BaseRepository<Persona>(dbContext);

    public IBaseRepository<PersonaHumana> PersonaHumanaRepository =>
        _personaHumanaRepository ??= new BaseRepository<PersonaHumana>(dbContext);

    public IBaseRepository<PersonaJuridica> PersonaJuridicaRepository =>
        _personaJuridicaRepository ??= new BaseRepository<PersonaJuridica>(dbContext);

    public IBaseRepository<MedioContacto> MedioContactoRepository =>
        _medioContactoRepository ??= new BaseRepository<MedioContacto>(dbContext);

    public IBaseRepository<Telefono> TelefonoRepository =>
        _telefonoRepository ??= new BaseRepository<Telefono>(dbContext);

    public IBaseRepository<Email> EmailRepository => _emailRepository ??= new BaseRepository<Email>(dbContext);

    public IBaseRepository<Whatsapp> WhatsappRepository =>
        _whatsappRepository ??= new BaseRepository<Whatsapp>(dbContext);

    public IBaseRepository<DocumentoIdentidad> DocumentoIdentidadRepository =>
        _documentoIdentidadRepository ??= new BaseRepository<DocumentoIdentidad>(dbContext);

    public IBaseRepository<Direccion> DireccionRepository =>
        _direccionRepository ??= new BaseRepository<Direccion>(dbContext);

    public IBaseRepository<Rol> RolRepository => _rolRepository ??= new BaseRepository<Rol>(dbContext);

    public IBaseRepository<UsuarioSistema> UsuarioSistemaRepository =>
        _usuarioSistemaRepository ??= new BaseRepository<UsuarioSistema>(dbContext);

    public IBaseRepository<PersonaVulnerable> PersonaVulnerableRepository =>
        _personaVulnerableRepository ??= new BaseRepository<PersonaVulnerable>(dbContext);

    public IBaseRepository<Tecnico> TecnicoRepository => _tecnicoRepository ??= new BaseRepository<Tecnico>(dbContext);

    public IBaseRepository<Colaborador> ColaboradorRepository =>
        _colaboradorRepository ??= new BaseRepository<Colaborador>(dbContext);

    public IBaseRepository<AreaCobertura> AreaCoberturaRepository =>
        _areaCoberturaRepository ??= new BaseRepository<AreaCobertura>(dbContext);

    public IBaseRepository<Tarjeta> TarjetaRepository => _tarjetaRepository ??= new BaseRepository<Tarjeta>(dbContext);

    public IBaseRepository<TarjetaConsumo> TarjetaConsumoRepository =>
        _tarjetaConsumoRepository ??= new BaseRepository<TarjetaConsumo>(dbContext);

    public IBaseRepository<TarjetaColaboracion> TarjetaColaboracionRepository =>
        _tarjetaColaboracionRepository ??= new BaseRepository<TarjetaColaboracion>(dbContext);

    public IBaseRepository<FormaContribucion> FormaContribucionRepository =>
        _formaContribucionRepository ??= new BaseRepository<FormaContribucion>(dbContext);

    public IBaseRepository<AdministracionHeladera> AdministracionHeladeraRepository =>
        _administracionHeladeraRepository ??= new BaseRepository<AdministracionHeladera>(dbContext);

    public IBaseRepository<DistribucionViandas> DistribucionViandasRepository =>
        _distribucionViandasRepository ??= new BaseRepository<DistribucionViandas>(dbContext);

    public IBaseRepository<RegistroPersonaVulnerable> RegistroPersonaVulnerableRepository =>
        _registroPersonaVulnerableRepository ??= new BaseRepository<RegistroPersonaVulnerable>(dbContext);

    public IBaseRepository<DonacionMonetaria> DonacionMonetariaRepository =>
        _donacionMonetariaRepository ??= new BaseRepository<DonacionMonetaria>(dbContext);

    public IBaseRepository<DonacionVianda> DonacionViandaRepository =>
        _donacionViandaRepository ??= new BaseRepository<DonacionVianda>(dbContext);

    public IBaseRepository<OfertaPremio> OfertaPremioRepository =>
        _ofertaPremioRepository ??= new BaseRepository<OfertaPremio>(dbContext);

    public IBaseRepository<Heladera> HeladeraRepository =>
        _heladeraRepository ??= new BaseRepository<Heladera>(dbContext);

    public IBaseRepository<Vianda> ViandaRepository => _viandaRepository ??= new BaseRepository<Vianda>(dbContext);

    public IBaseRepository<ViandaEstandar> ViandaEstandarRepository =>
        _viandaEstandarRepository ??= new BaseRepository<ViandaEstandar>(dbContext);

    public IBaseRepository<PuntoEstrategico> PuntoEstrategicoRepository =>
        _puntoEstrategicoRepository ??= new BaseRepository<PuntoEstrategico>(dbContext);

    public IBaseRepository<ModeloHeladera> ModeloHeladeraRepository =>
        _modeloHeladeraRepository ??= new BaseRepository<ModeloHeladera>(dbContext);

    public IBaseRepository<Sensor> SensorRepository => _sensorRepository ??= new BaseRepository<Sensor>(dbContext);

    public IBaseRepository<SensorMovimiento> SensorMovimientoRepository =>
        _sensorMovimientoRepository ??= new BaseRepository<SensorMovimiento>(dbContext);

    public IBaseRepository<SensorTemperatura> SensorTemperaturaRepository =>
        _sensorTemperaturaRepository ??= new BaseRepository<SensorTemperatura>(dbContext);

    public IBaseRepository<RegistroMovimiento> RegistroMovimientoRepository =>
        _registroMovimientoRepository ??= new BaseRepository<RegistroMovimiento>(dbContext);

    public IBaseRepository<RegistroTemperatura> RegistroTemperaturaRepository =>
        _registroTemperaturaRepository ??= new BaseRepository<RegistroTemperatura>(dbContext);

    public IBaseRepository<Incidente> IncidenteRepository =>
        _incidenteRepository ??= new BaseRepository<Incidente>(dbContext);

    public IBaseRepository<VisitaTecnica> VisitaTecnicaRepository =>
        _visitaTecnicaRepository ??= new BaseRepository<VisitaTecnica>(dbContext);

    public IBaseRepository<Premio> PremioRepository => _premioRepository ??= new BaseRepository<Premio>(dbContext);

    public IBaseRepository<Notificacion> NotificacionRepository =>
        _notificacionRepository ??= new BaseRepository<Notificacion>(dbContext);

    public IBaseRepository<Suscripcion> SuscripcionRepository =>
        _suscripcionRepository ??= new BaseRepository<Suscripcion>(dbContext);

    public IBaseRepository<SuscripcionFaltanteViandas> SuscripcionFaltanteViandasRepository =>
        _suscripcionFaltanteViandasRepository ??= new BaseRepository<SuscripcionFaltanteViandas>(dbContext);

    public IBaseRepository<SuscripcionExcedenteViandas> SuscripcionExcedenteViandasRepository =>
        _suscripcionExcedenteViandasRepository ??= new BaseRepository<SuscripcionExcedenteViandas>(dbContext);

    public IBaseRepository<SuscripcionIncidenteHeladera> SuscripcionIncidenteHeladeraRepository =>
        _suscripcionIncidenteHeladeraRepository ??= new BaseRepository<SuscripcionIncidenteHeladera>(dbContext);

    public IBaseRepository<AccesoHeladera> AccesoHeladeraRepository =>
        _accesoHeladeraRepository ??= new BaseRepository<AccesoHeladera>(dbContext);

    public IBaseRepository<AutorizacionManipulacionHeladera> AutorizacionManipulacionHeladeraRepository =>
        _autorizacionManipulacionHeladeraRepository ??= new BaseRepository<AutorizacionManipulacionHeladera>(dbContext);

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await dbContext.DisposeAsync();
    }

    public Task BeginTransactionAsync()
    {
        return dbContext.Database.BeginTransactionAsync();
    }

    public Task CommitTransactionAsync()
    {
        return dbContext.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
        return dbContext.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}