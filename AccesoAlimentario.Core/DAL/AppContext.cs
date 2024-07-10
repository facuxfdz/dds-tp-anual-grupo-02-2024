using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
    public DbSet<Heladera> Heladeras { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
}