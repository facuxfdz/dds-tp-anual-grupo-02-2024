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
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Personas
    public DbSet<Persona> Personas { get; set; } = null!;
    public DbSet<Colaborador> Colaboradores { get; set; } = null!;

    // Medios de Comunicacion
    public DbSet<MedioContacto> MediosContacto { get; set; } = null!;

    // Documentos de Identidad
    public DbSet<DocumentoIdentidad> DocumentosIdentidad { get; set; } = null!;

    // Direcciones
    public DbSet<Direccion> Direcciones { get; set; } = null!;

    // Roles
    public DbSet<Rol> Roles { get; set; } = null!;
    public DbSet<AreaCobertura> AreasCobertura { get; set; } = null!;

    // Tarjetas
    public DbSet<Tarjeta> Tarjetas { get; set; } = null!;

    // Contribuciones
    public DbSet<FormaContribucion> FormasContribucion { get; set; } = null!;

    // Heladeras
    public DbSet<Heladera> Heladeras { get; set; } = null!;
    public DbSet<Vianda> Viandas { get; set; } = null!;
    public DbSet<ViandaEstandar> ViandasEstandar { get; set; } = null!;
    public DbSet<PuntoEstrategico> PuntosEstrategicos { get; set; } = null!;
    public DbSet<ModeloHeladera> ModelosHeladera { get; set; } = null!;

    // Sensores
    public DbSet<Sensor> Sensores { get; set; } = null!;
    public DbSet<RegistroMovimiento> RegistrosMovimiento { get; set; } = null!;
    public DbSet<RegistroTemperatura> RegistrosTemperatura { get; set; } = null!;

    // Incidentes
    public DbSet<Incidente> Incidentes { get; set; } = null!;
    public DbSet<VisitaTecnica> VisitasTecnicas { get; set; } = null!;

    // Premios
    public DbSet<Premio> Premios { get; set; } = null!;

    // Notificaciones
    public DbSet<Notificacion> Notificaciones { get; set; } = null!;

    // Suscripciones Colaboradores
    public DbSet<Suscripcion> Suscripciones { get; set; } = null!;

    // Autorizaciones
    public DbSet<AccesoHeladera> AccesosHeladera { get; set; } = null!;
    public DbSet<AutorizacionManipulacionHeladera> AutorizacionesManipulacionHeladera { get; set; } = null!;
    
    // Reportes
    public DbSet<Reporte> Reportes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>()
            .HasDiscriminator<string>("Discriminador")
            .HasValue<PersonaHumana>("Humana")
            .HasValue<PersonaJuridica>("Juridica");

        modelBuilder.Entity<MedioContacto>()
            .HasDiscriminator<string>("Discriminador")
            .HasValue<Telefono>("Telefono")
            .HasValue<Email>("Email")
            .HasValue<Whatsapp>("Whatsapp");

        modelBuilder.Entity<Rol>()
            .UseTpcMappingStrategy();
        modelBuilder.Entity<UsuarioSistema>().ToTable("UsuariosSistema");
        modelBuilder.Entity<PersonaVulnerable>().ToTable("PersonasVulnerables");
        modelBuilder.Entity<Tecnico>().ToTable("Tecnicos");
        modelBuilder.Entity<Colaborador>().ToTable("Colaboradores");

        modelBuilder.Entity<Tarjeta>()
            .HasDiscriminator<string>("Discriminador")
            .HasValue<TarjetaConsumo>("Consumo")
            .HasValue<TarjetaColaboracion>("Colaboracion");

        modelBuilder.Entity<FormaContribucion>()
            .UseTpcMappingStrategy();
        modelBuilder.Entity<AdministracionHeladera>().ToTable("AdministracionesHeladera");
        modelBuilder.Entity<DistribucionViandas>().ToTable("DistribucionesViandas");
        modelBuilder.Entity<RegistroPersonaVulnerable>().ToTable("RegistrosPersonasVulnerables");
        modelBuilder.Entity<DonacionMonetaria>().ToTable("DonacionesMonetarias");
        modelBuilder.Entity<DonacionVianda>().ToTable("DonacionesViandas");
        modelBuilder.Entity<OfertaPremio>().ToTable("OfertasPremios");

        modelBuilder.Entity<Sensor>()
            .UseTptMappingStrategy();
        modelBuilder.Entity<SensorMovimiento>().ToTable("SensoresMovimiento");
        modelBuilder.Entity<SensorTemperatura>().ToTable("SensoresTemperatura");

        modelBuilder.Entity<Incidente>()
            .HasDiscriminator<string>("Discriminador")
            .HasValue<FallaTecnica>("FallaTecnica")
            .HasValue<Alerta>("Alerta");

        modelBuilder.Entity<Suscripcion>()
            .HasDiscriminator<string>("Discriminador")
            .HasValue<SuscripcionFaltanteViandas>("FaltanteViandas")
            .HasValue<SuscripcionExcedenteViandas>("ExcedenteViandas")
            .HasValue<SuscripcionIncidenteHeladera>("IncidenteHeladera");
        
        // Evitar ciclos en la eliminacion en cascada
        modelBuilder.Entity<DistribucionViandas>()
            .HasOne(d => d.HeladeraOrigen)
            .WithMany()
            .HasForeignKey("HeladeraOrigenId")
            .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.NoAction

        modelBuilder.Entity<DistribucionViandas>()
            .HasOne(d => d.HeladeraDestino)
            .WithMany()
            .HasForeignKey("HeladeraDestinoId")
            .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.NoAction
        
        modelBuilder.Entity<PersonaVulnerable>()
            .HasOne(p => p.Tarjeta)
            .WithMany()
            .HasForeignKey("TarjetaId")
            .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.NoAction
        
        modelBuilder.Entity<DonacionVianda>()
            .HasOne(d => d.Vianda)
            .WithMany()
            .HasForeignKey("ViandaId")
            .OnDelete(DeleteBehavior.Restrict);  // O DeleteBehavior.NoAction

        modelBuilder.Entity<DonacionVianda>()
            .HasOne(d => d.Heladera)
            .WithMany()
            .HasForeignKey("HeladeraId")
            .OnDelete(DeleteBehavior.Restrict);  // O DeleteBehavior.NoAction
    }
}