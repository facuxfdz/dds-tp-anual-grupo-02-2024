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
    /*public DbSet<Heladera> Heladeras { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<PuntoEstrategico> PuntosEstrategicos { get; set; }
    public DbSet<Vianda> Viandas { get; set; }*/
    public DbSet<PersonaHumana> PersonasHumanas { get; set; }
    /*public DbSet<PersonaJuridica> PersonasJuridicas { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<PersonaVulnerable> PersonasVulnerables { get; set; }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedioContacto>()
            .HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
}