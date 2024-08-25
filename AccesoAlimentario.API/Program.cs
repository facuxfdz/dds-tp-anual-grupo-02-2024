
using System.Text.Json.Serialization;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Infrastructure.DAL;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.Colaboradores;
using AccesoAlimentario.API.UseCases.Personas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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

builder.Services.AddScoped<IRepository<Colaborador>, GenericRepository<Colaborador>>();
builder.Services.AddScoped<IRepository<Persona>, GenericRepository<Persona>>();
builder.Services.AddScoped<CrearColaboradorHTTP>();
builder.Services.AddScoped<CrearPersona>();

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
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
