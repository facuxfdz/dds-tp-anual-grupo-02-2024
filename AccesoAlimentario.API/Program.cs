
using System.Text;
using System.Text.Json.Serialization;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Incidentes;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Domain.Premios;
using AccesoAlimentario.API.Domain.Tecnicos;
using AccesoAlimentario.API.Infrastructure.DAL;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.AccesoHeladera;
using AccesoAlimentario.API.UseCases.Colaboradores;
using AccesoAlimentario.API.UseCases.Heladeras;
using AccesoAlimentario.API.UseCases.Incidentes;
using AccesoAlimentario.API.UseCases.Personas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>((provider, options) =>
{
    options.UseMySQL(
        "server=localhost;port=3346;database=acceso_alimentario_nueva;user=root;password=pass123");
    // options.UseInMemoryDatabase(databaseName: "AccesoAlimentario");
});
builder.Services.AddScoped(typeof(UnitOfWork));
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(GenericRepository<Colaborador>));
builder.Services.AddScoped(typeof(GenericRepository<Persona>));

var encryptionKey = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
// TODO: Separar y agrupar los servicios para mejor legibilidad (Por ejemplo para ver facilmente la inyeccion de dependencias)
builder.Services.AddScoped<IRepository<Colaborador>, GenericRepository<Colaborador>>();
builder.Services.AddScoped<IRepository<Persona>, GenericRepository<Persona>>();
builder.Services.AddScoped<IRepository<Heladera>, GenericRepository<Heladera>>();
builder.Services.AddScoped<IRepository<Tarjeta>, GenericRepository<Tarjeta>>();
builder.Services.AddScoped<IRepository<TarjetaColaboracion>, GenericRepository<TarjetaColaboracion>>();
builder.Services.AddScoped<IRepository<ModeloHeladera>, GenericRepository<ModeloHeladera>>();
builder.Services.AddScoped<IRepository<PuntoEstrategico>, GenericRepository<PuntoEstrategico>>();
builder.Services.AddScoped<IRepository<AutorizacionHeladera>, GenericRepository<AutorizacionHeladera>>();
builder.Services.AddScoped<IRepository<AccesoHeladera>, GenericRepository<AccesoHeladera>>();

builder.Services.AddScoped<IRepository<AdministracionHeladera>, GenericRepository<AdministracionHeladera>>();
builder.Services.AddScoped<IRepository<DonacionMonetaria>, GenericRepository<DonacionMonetaria>>();
builder.Services.AddScoped<IRepository<DonacionVianda>, GenericRepository<DonacionVianda>>();
builder.Services.AddScoped<IRepository<RegistroPersonaVulnerable>, GenericRepository<RegistroPersonaVulnerable>>();
builder.Services.AddScoped<IRepository<OfertaPremio>, GenericRepository<OfertaPremio>>();
builder.Services.AddScoped<IRepository<Premio>, GenericRepository<Premio>>();
builder.Services.AddScoped<IRepository<FallaTecnica>, GenericRepository<FallaTecnica>>();
builder.Services.AddScoped<IRepository<Tecnico>, GenericRepository<Tecnico>>();
builder.Services.AddScoped<IRepository<Incidente>, GenericRepository<Incidente>>();
builder.Services.AddScoped<IRepository<VisitaTecnica>, GenericRepository<VisitaTecnica>>();

builder.Services.AddScoped<IGeneradorCodigoTarjeta, GenerarRandom>();
builder.Services.AddScoped<IEncryptionService>(provider => new AuthenticatedEncryptionService(encryptionKey));
builder.Services.AddScoped<CrearTarjetaColaboracion>();
builder.Services.AddScoped<CrearColaboradorHTTP>();
builder.Services.AddScoped<CrearPersona>();
builder.Services.AddScoped<AutorizacionHeladera>();
builder.Services.AddScoped<AutorizarAccesoHeladera>();
builder.Services.AddScoped<CrearHeladera>();
builder.Services.AddScoped<CrearModeloHeladera>();
builder.Services.AddScoped<DarAltaPuntoHeladera>();
builder.Services.AddScoped<RegistrarAccesoHeladera>();
builder.Services.AddScoped<ImportarColaboraciones>();
builder.Services.AddScoped<CrearVisitaTecnica>();
builder.Services.AddScoped<CrearFallaTecnica>();

builder.Services
    .AddMvc()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });
//
// builder.Services.AddScoped(typeof(UnitOfWork));
// builder.Services.AddScoped(typeof(GenericRepository<>));
//
// /* -------------------------- */
// builder.Services.AddScoped(typeof(PersonasServicio));
// builder.Services.AddScoped(typeof(PuntoEstrategicoServicio));
// builder.Services.AddScoped(typeof(DireccionServicio));
// builder.Services.AddScoped(typeof(HeladerasServicio));
// builder.Services.AddScoped(typeof(ModeloHeladeraServicio));
//
// // Register RabbitMQ background service
// builder.Services.AddScoped(typeof(GenericRepository<RegistroFraude>));
// builder.Services.AddScoped(typeof(GenericRepository<RegistroTemperatura>));
// builder.Services.AddScoped(typeof(RegistrarFraudeHeladera));
// builder.Services.AddScoped(typeof(RegistrarTemperaturaHeladera));
// builder.Services.AddHostedService<RabbitMQBackgroundService>();

// Allow CORS
const string corsDevelop = "_CORSDevelop";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsDevelop,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors(corsDevelop);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
