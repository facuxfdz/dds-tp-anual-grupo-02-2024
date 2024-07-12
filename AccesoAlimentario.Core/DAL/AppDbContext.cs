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
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AccesoHeladera> AccesosHeladeras { get; set; } = null!;
    public DbSet<AutorizacionManipulacionHeladera> AutorizacionesManipulacionHeladeras { get; set; } = null!;
    public DbSet<AdministracionHeladera> AdministracionesHeladeras { get; set; } = null!;
    public DbSet<DistribucionViandas> DistribucionesViandas { get; set; } = null!;
    public DbSet<DonacionMonetaria> DonacionesMonetarias { get; set; } = null!;
    public DbSet<DonacionVianda> DonacionesViandas { get; set; } = null!;
    public DbSet<OfertaPremio> OfertasPremios { get; set; } = null!;
    public DbSet<RegistroPersonaVulnerable> RegistrosPersonasVulnerables { get; set; } = null!;
    public DbSet<Direccion> Direcciones { get; set; } = null!;
    public DbSet<DocumentoIdentidad> DocumentosIdentidad { get; set; } = null!;
    public DbSet<Heladera> Heladeras { get; set; } = null!;
    public DbSet<ModeloHeladera> ModelosHeladeras { get; set; } = null!;
    public DbSet<PuntoEstrategico> PuntosEstrategicos { get; set; } = null!;
    public DbSet<Vianda> Viandas { get; set; } = null!;
    public DbSet<ViandaEstandar> ViandasEstandares { get; set; } = null!;
    public DbSet<Alerta> Alertas { get; set; } = null!;
    public DbSet<FallaTecnica> FallasTecnicas { get; set; } = null!;
    public DbSet<Incidente> Incidentes { get; set; } = null!;
    public DbSet<VisitaTecnica> VisitasTecnicas { get; set; } = null!;
    public DbSet<Email> Emails { get; set; } = null!;
    public DbSet<MedioContacto> MediosContacto { get; set; } = null!;
    public DbSet<Telefono> Telefonos { get; set; } = null!;
    public DbSet<Whatsapp> Whatsapps { get; set; } = null!;
    public DbSet<Notificacion> Notificaciones { get; set; } = null!;
    public DbSet<PersonaHumana> PersonasHumanas { get; set; } = null!;
    public DbSet<PersonaJuridica> PersonasJuridicas { get; set; } = null!;
    public DbSet<Premio> Premios { get; set; } = null!;
    public DbSet<AreaCobertura> AreasCobertura { get; set; } = null!;
    public DbSet<Colaborador> Colaboradores { get; set; } = null!;
    public DbSet<PersonaVulnerable> PersonasVulnerables { get; set; } = null!;
    public DbSet<Rol> Roles { get; set; } = null!;
    public DbSet<Tecnico> Tecnicos { get; set; } = null!;
    public DbSet<UsuarioSistema> UsuariosSistema { get; set; } = null!;
    public DbSet<RegistroMovimiento> RegistrosMovimientos { get; set; } = null!;
    public DbSet<RegistroTemperatura> RegistrosTemperaturas { get; set; } = null!;
    public DbSet<SensorMovimiento> SensoresMovimientos { get; set; } = null!;
    public DbSet<SensorTemperatura> SensoresTemperaturas { get; set; } = null!;
    public DbSet<Suscripcion> Suscripciones { get; set; } = null!;
    public DbSet<SuscripcionExcedenteViandas> SuscripcionesExcedenteViandas { get; set; } = null!;
    public DbSet<SuscripcionFaltanteViandas> SuscripcionesFaltanteViandas { get; set; } = null!;
    public DbSet<SuscripcionIncidenteHeladera> SuscripcionesIncidenteHeladeras { get; set; } = null!;
    public DbSet<Tarjeta> Tarjetas { get; set; } = null!;
    public DbSet<TarjetaColaboracion> TarjetasColaboracion { get; set; } = null!;
    public DbSet<TarjetaConsumo> TarjetasConsumo { get; set; } = null!;
}