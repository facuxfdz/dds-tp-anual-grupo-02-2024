using AccesoAlimentario.Core;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations;
using AccesoAlimentario.Web.Swagger;
using Microsoft.EntityFrameworkCore;
using AccesoAlimentario.Web.SecretRetrieve;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Serilog;
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
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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
                           $"user={dbConnectionData.DB_USER};" +
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
        var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "192.168.1.41";
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "AccesoAlimentario";
        var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "YourStrongPassword!123";
        connectionString = $"server={dbServer};" +
                           $"database={dbName};" +
                           $"user={dbUser};" +
                           $"password={dbPassword};";
    }
}


builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options
        .UseMySQL(connectionString, x =>
        {
            x.MigrationsAssembly("AccesoAlimentario.Core");
            x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        })
        .UseLazyLoadingProxies()
);

// Add services to the container.
builder.Services.AddControllersWithViews();

var authenticationBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

authenticationBuilder
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        // configuring cookie options
        options.Cookie.Name = "google-auth";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
        // Redirect to login page if user is not authenticated
        options.LoginPath = "/";
        // Redirect to access denied page if user is not authorized
        options.AccessDeniedPath = "/Error";
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme,options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")) ||
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")))
            {
                options.ClientId = builder.Configuration.GetSection("Google").GetValue<string>("ClientId");
                options.ClientSecret = builder.Configuration.GetSection("Google").GetValue<string>("ClientSecret");
            }
            else
            {
                options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
            }
        }
        else
        {
            var awsSecretsManager = new SecretRetrieve();
            options.ClientId = awsSecretsManager.GetSecret("acceso_alimentario/google_client_id");
            options.ClientSecret = awsSecretsManager.GetSecret("acceso_alimentario/google_client_secret");
        }
    });

// Allow CORS
const string corsDevelop = "_CORSDevelop";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsDevelop,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddCoreLayer();
builder.Services.AddOperationsLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.UseCors(corsDevelop);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
