/* Inicio de repositorio y servicio */

using AccesoAlimentario.Core.DAL;
using Microsoft.EntityFrameworkCore;

const string testCsv = @"Test//csvE2.csv";

var options = new DbContextOptionsBuilder<AppDbContext>(options: new DbContextOptions<AppDbContext>())
    .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
    .Options;

var dbcontext = new AppDbContext(options);

var unitOfWork = new UnitOfWork(dbcontext);
