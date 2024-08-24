using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Suscripciones;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.API.Infrastructure.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Colaborador> Colaboradores { get; set; } = null!;
    public DbSet<Contribucion> Contribuciones { get; set; } = null!;
    public DbSet<Suscripciones> Suscripciones { get; set; } = null!;
    public DbSet<TarjetaColaboracion> TarjetasColaboracion { get; set; } = null!;
}