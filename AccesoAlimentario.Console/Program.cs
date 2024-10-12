/* Inicio de repositorio y servicio */

using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using Microsoft.EntityFrameworkCore;

const string testCsv = @"Test//csvE2.csv";

var options = new DbContextOptionsBuilder<AppDbContext>(options: new DbContextOptions<AppDbContext>())
    .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
    .Options;

var dbcontext = new AppDbContext(options);

var unitOfWork = new UnitOfWork(dbcontext);
