/* Inicio de repositorio y servicio */

using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Servicios;
using Microsoft.EntityFrameworkCore;

const string testCsv = @"Test//csvE2.csv";

var options = new DbContextOptionsBuilder<AppDbContext>(options: new DbContextOptions<AppDbContext>())
    .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
    .Options;

var dbcontext = new AppDbContext(options);

var unitOfWork = new UnitOfWork(dbcontext);

var personasServicio = new PersonasServicio(unitOfWork);
var colaboradoresServicio = new ColaboradoresServicio(unitOfWork, personasServicio);
var autorizacionesServicio = new AutorizacionesServicio(unitOfWork);
var colaboracionesServicio = new ColaboracionesServicio(unitOfWork, colaboradoresServicio);
var heladerasServicio = new HeladerasServicio(unitOfWork);
var importadorServicio = new ImportadorServicio(unitOfWork, personasServicio, colaboradoresServicio);
var personasVulnerablesServicio = new PersonasVulnerablesServicio(unitOfWork);
var premiosServicio = new PremiosServicio(unitOfWork);
var recomendacionesServicio = new RecomendacionesServicio();
var sensoreoServicio = new SensoreoServicio(unitOfWork);
var tecnicosServicio = new TecnicosServicio(unitOfWork);
var usuariosServicio = new UsuariosSistemaServicio(unitOfWork);

// Crear persona humana
personasServicio.CrearHumana
(
    "Juan",
    new Direccion
    (
        "Calle",
        "123",
        "Avellaneda",
        "A123B"
    ),
    new DocumentoIdentidad(TipoDocumento.Dni, 123456789, DateOnly.MinValue),
    new List<MedioContacto>
    {
        new Email(true, "mpedaci@frba.utn.edu.ar")
    },
    "Perez",
    SexoDocumento.Masculino
);

// Crear persona juridica
personasServicio.CrearJuridica
(
    "Empresa",
    "SA",
    new Direccion
    (
        "Calle",
        "123",
        "Avellaneda",
        "A123B"
    ),
    new DocumentoIdentidad(TipoDocumento.Cuit, 123456789, DateOnly.MinValue),
    new List<MedioContacto>
    {
        new Email(true, "mpedaci@frba.utn.edu.ar")
    },
    TipoJuridico.Empresa,
    "Rubro"
);

// Obtener personas
var persona = personasServicio.Obtener();
persona.First().EnviarNotificacion(new Notificacion("Mensaje de prueba", "Mensaje de prueba de notificación"));

// Obtener recomendaciones

var recomendaciones = recomendacionesServicio.ObtenerPuntosRecomendados(1, 2, 3);

// Importar archivo

var bytes = File.ReadAllBytes(testCsv);
var base64 = Convert.ToBase64String(bytes);

importadorServicio.Importar(base64);

persona = personasServicio.Obtener();

var username = Console.ReadLine();
var password = Console.ReadLine();

var usuarios = unitOfWork.UsuarioSistemaRepository.Get();

var usuario = usuariosServicio.Login(username, password);




Console.WriteLine("Fin de la ejecución");
