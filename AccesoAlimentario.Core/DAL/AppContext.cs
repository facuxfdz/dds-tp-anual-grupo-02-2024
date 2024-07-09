using AccesoAlimentario.Core.Entities.Heladeras;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class AppContext : DbContext
{
    public DbSet<Heladera> Heladeras { get; set; }
}