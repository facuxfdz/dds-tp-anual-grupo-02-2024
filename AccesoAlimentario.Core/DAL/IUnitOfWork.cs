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
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.DAL;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Persona> PersonaRepository { get; }
    public IBaseRepository<PersonaHumana> PersonaHumanaRepository { get; }
    public IBaseRepository<PersonaJuridica> PersonaJuridicaRepository { get; }
    public IBaseRepository<MedioContacto> MedioContactoRepository { get; }
    public IBaseRepository<Telefono> TelefonoRepository { get; }
    public IBaseRepository<Email> EmailRepository { get; }
    public IBaseRepository<Whatsapp> WhatsappRepository { get; }
    public IBaseRepository<DocumentoIdentidad> DocumentoIdentidadRepository { get; }
    public IBaseRepository<Direccion> DireccionRepository { get; }
    public IBaseRepository<Rol> RolRepository { get; }
    public IBaseRepository<UsuarioSistema> UsuarioSistemaRepository { get; }
    public IBaseRepository<PersonaVulnerable> PersonaVulnerableRepository { get; }
    public IBaseRepository<Tecnico> TecnicoRepository { get; }
    public IBaseRepository<Colaborador> ColaboradorRepository { get; }
    public IBaseRepository<AreaCobertura> AreaCoberturaRepository { get; }
    public IBaseRepository<Tarjeta> TarjetaRepository { get; }
    public IBaseRepository<TarjetaConsumo> TarjetaConsumoRepository { get; }
    public IBaseRepository<TarjetaColaboracion> TarjetaColaboracionRepository { get; }
    public IBaseRepository<FormaContribucion> FormaContribucionRepository { get; }
    public IBaseRepository<AdministracionHeladera> AdministracionHeladeraRepository { get; }
    public IBaseRepository<DistribucionViandas> DistribucionViandasRepository { get; }
    public IBaseRepository<RegistroPersonaVulnerable> RegistroPersonaVulnerableRepository { get; }
    public IBaseRepository<DonacionMonetaria> DonacionMonetariaRepository { get; }
    public IBaseRepository<DonacionVianda> DonacionViandaRepository { get; }
    public IBaseRepository<OfertaPremio> OfertaPremioRepository { get; }
    public IBaseRepository<Heladera> HeladeraRepository { get; }
    public IBaseRepository<Vianda> ViandaRepository { get; }
    public IBaseRepository<ViandaEstandar> ViandaEstandarRepository { get; }
    public IBaseRepository<PuntoEstrategico> PuntoEstrategicoRepository { get; }
    public IBaseRepository<ModeloHeladera> ModeloHeladeraRepository { get; }
    public IBaseRepository<Sensor> SensorRepository { get; }
    public IBaseRepository<SensorMovimiento> SensorMovimientoRepository { get; }
    public IBaseRepository<SensorTemperatura> SensorTemperaturaRepository { get; }
    public IBaseRepository<RegistroMovimiento> RegistroMovimientoRepository { get; }
    public IBaseRepository<RegistroTemperatura> RegistroTemperaturaRepository { get; }
    public IBaseRepository<Incidente> IncidenteRepository { get; }
    public IBaseRepository<VisitaTecnica> VisitaTecnicaRepository { get; }
    public IBaseRepository<Premio> PremioRepository { get; }
    public IBaseRepository<Notificacion> NotificacionRepository { get; }
    public IBaseRepository<Suscripcion> SuscripcionRepository { get; }
    public IBaseRepository<SuscripcionFaltanteViandas> SuscripcionFaltanteViandasRepository { get; }
    public IBaseRepository<SuscripcionExcedenteViandas> SuscripcionExcedenteViandasRepository { get; }
    public IBaseRepository<SuscripcionIncidenteHeladera> SuscripcionIncidenteHeladeraRepository { get; }
    public IBaseRepository<AccesoHeladera> AccesoHeladeraRepository { get; }
    public IBaseRepository<AutorizacionManipulacionHeladera> AutorizacionManipulacionHeladeraRepository { get; }
    public IBaseRepository<Reporte> ReporteRepository { get; }

    public Task SaveChangesAsync();

    public Task RollbackAsync();

    public Task BeginTransactionAsync();

    public Task CommitTransactionAsync();

    public Task RollbackTransactionAsync();
}