using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas.PersonaJuridica;
using AccesoAlimentario.Core.Entities.Personas.Tecnicos;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<AccesoHeladera> AccesoHeladeras { get; set; }
    public DbSet<AutorizacionManipulacionHeladera> AutorizacionManipulacionHeladeras { get; set; }
    public DbSet<AdministracionHeladera> AdministracionHeladeras { get; set; }
    public DbSet<DistribucionViandas> DistribucionViandass { get; set; }
    public DbSet<DonacionMonetaria> DonacionMonetarias { get; set; }
    public DbSet<DonacionVianda> DonacionViandas { get; set; }
    public DbSet<FormaContribucion> FormaContribucions { get; set; }
    public DbSet<OfertaPremio> OfertaPremios { get; set; }
    public DbSet<RegistroPersonaVulnerable> RegistroPersonaVulnerables { get; set; }
    public DbSet<Direccion> Direccions { get; set; }
    public DbSet<DocumentoIdentidad> DocumentoIdentidads { get; set; }
    public DbSet<Heladera> Heladeras { get; set; }
    public DbSet<ModeloHeladera> ModeloHeladeras { get; set; }
    public DbSet<PuntoEstrategico> PuntoEstrategicos { get; set; }
    public DbSet<Vianda> Viandas { get; set; }
    public DbSet<ViandaEstandar> ViandaEstandars { get; set; }
    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<FallaTecnica> FallaTecnicas { get; set; }
    public DbSet<Incidente> Incidentes { get; set; }
    public DbSet<VisitaTecnica> VisitaTecnicas { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<MedioContacto> MedioContactos { get; set; }
    public DbSet<Telefono> Telefonos { get; set; }
    public DbSet<Whatsapp> Whatsapps { get; set; }
    public DbSet<Notificacion> Notificacions { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<PersonaHumana> PersonaHumanas { get; set; }
    public DbSet<PersonaJuridica> PersonaJuridicas { get; set; }
    public DbSet<Premio> Premios { get; set; }
    public DbSet<AreaCobertura> AreaCoberturas { get; set; }
    public DbSet<Colaborador> Colaboradors { get; set; }
    public DbSet<PersonaVulnerable> PersonaVulnerables { get; set; }
    public DbSet<Rol> Rols { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<UsuarioSistema> UsuarioSistemas { get; set; }
    public DbSet<RegistroMovimiento> RegistroMovimientos { get; set; }
    public DbSet<RegistroTemperatura> RegistroTemperaturas { get; set; }
    public DbSet<ISensor> Sensors { get; set; }
    public DbSet<SensorMovimiento> SensorMovimientos { get; set; }
    public DbSet<SensorTemperatura> SensorTemperaturas { get; set; }
    public DbSet<Suscripcion> Suscripcions { get; set; }
    public DbSet<SuscripcionExcedenteViandas> SuscripcionExcedenteViandass { get; set; }
    public DbSet<SuscripcionFaltanteViandas> SuscripcionFaltanteViandass { get; set; }
    public DbSet<SuscripcionIncidenteHeladera> SuscripcionIncidenteHeladeras { get; set; }
    public DbSet<Tarjeta> Tarjetas { get; set; }
    public DbSet<TarjetaColaboracion> TarjetaColaboracions { get; set; }
    public DbSet<TarjetaConsumo> TarjetaConsumos { get; set; }
}