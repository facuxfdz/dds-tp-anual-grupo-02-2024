using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Suscripciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Incidentes;
using AccesoAlimentario.API.Domain.Notificaciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.Core.Entities.Heladeras;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.API.Infrastructure.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Colaborador> Colaboradores { get; set; } = null!;
    public DbSet<Contribucion> Contribuciones { get; set; } = null!;
    public DbSet<DistribucionViandas> DistribucionesViandas { get; set; } = null!;
    public DbSet<Heladera> Heladeras { get; set; } = null!;
    public DbSet<ModeloHeladera> ModelosHeladera { get; set; } = null!;
    public DbSet<PuntoEstrategico> PuntosEstrategicos { get; set; } = null!;
    public DbSet<Vianda> Viandas { get; set; } = null!;
    public DbSet<ViandaEstandar> ViandasEstandar { get; set; } = null!;
    public DbSet<EventoHeladera> Suscripciones { get; set; } = null!;
    public DbSet<ViandasRestantes> ViandasRestantes { get; set; } = null!;
    public DbSet<TarjetaColaboracion> TarjetasColaboracion { get; set; } = null!;
    public DbSet<TarjetaConsumo> TarjetasConsumo { get; set; } = null!;
    public DbSet<Direccion> Direcciones { get; set; } = null!;
    public DbSet<Persona> Personas { get; set; } = null!;
    public DbSet<PersonaHumana> PersonasHumanas { get; set; } = null!;
    public DbSet<PersonaJuridica> PersonasJuridicas { get; set; } = null!;
    public DbSet<CanalNotificacion> CanalesNotificacion { get; set; } = null!;
    public DbSet<Email> Emails { get; set; } = null!;
    public DbSet<DocumentoIdentidad> DocumentosIdentidad { get; set; } = null!;
    public DbSet<AutorizacionHeladera> AutorizacionesHeladera { get; set; } = null!;
    public DbSet<AccesoHeladera> AccesosHeladera { get; set; } = null!;
    public DbSet<Alerta> Alertas { get; set; } = null!;
    public DbSet<FallaConexion> FallasConexion { get; set; } = null!;
    public DbSet<TemperaturaInusual> TemperaturasInusual { get; set; } = null!;
    public DbSet<MovimientoInusual> MovimientosInusual { get; set; } = null!;
    public DbSet<Incidente> Incidentes { get; set; } = null!;
    public DbSet<VisitaTecnica> VisitasTecnicas { get; set; } = null!;
    
    
    
}