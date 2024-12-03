using AccesoAlimentario.Core;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations;
using AccesoAlimentario.Web.Swagger;
using Microsoft.EntityFrameworkCore;
using AccesoAlimentario.Web.SecretRetrieve;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
);

// Add Swagger
builder.Services.AddSwaggerService();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

string connectionString;
// Obtener parametros de conexion de AWS Secrets Manager (AWS) si se encuentra en ambiente de producción
if (builder.Environment.IsProduction())
{
    var awsSecretsManager = new SecretRetrieve();
    var dbConnectionData = awsSecretsManager.GetSecretAs<DbConnectionData>("acceso_alimentario/db_connection_data");

    if (dbConnectionData != null)
    {
        connectionString = $"server={dbConnectionData.DB_SERVER};" +
                           $"database={dbConnectionData.DB_NAME};" +
                           $"user={dbConnectionData.DB_USERNAME};" +
                           $"password={dbConnectionData.DB_PASSWORD};";
    }
    else
    {
        throw new Exception("No se pudo obtener los datos de conexión desde Secrets Manager.");
    }
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
    if (string.IsNullOrEmpty(connectionString))
    {
        var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "AccesoAlimentario";
        var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "dev";
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "YourStrongPassword!123";
        connectionString = $"server={dbServer};" +
                           $"database={dbName};" +
                           $"user={dbUser};" +
                           $"password={dbPassword};";
    }
}

Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options
        .UseMySQL(connectionString, x =>
        {
            x.MigrationsAssembly("AccesoAlimentario.Core");
            x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        })
        .UseLazyLoadingProxies()
);

    
// Allow CORS
var corsDevelop = "_CORSDevelop";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsDevelop,
        policy =>
        {
            policy
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000")
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod();
        });
});
builder.Services.AddCoreLayer();
builder.Services.AddOperationsLayer();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(corsDevelop);

using (var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    int retries = 5;
    for (int i = 0; i < retries; i++)
    {
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (i == retries - 1)
            {
                throw;
            }
            Thread.Sleep(10000);
        }
    }
    
}


app.UseSwaggerConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();