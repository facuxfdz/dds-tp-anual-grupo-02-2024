using AccesoAlimentario.Core;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations;
using AccesoAlimentario.Web.Swagger;
using Microsoft.EntityFrameworkCore;
using AccesoAlimentario.Web.SecretRetrieve;

var builder = WebApplication.CreateBuilder(args);

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
        connectionString = $"Data Source={dbConnectionData.DB_SERVER};" +
                           $"Initial Catalog={dbConnectionData.DB_NAME};" +
                           $"User ID={dbConnectionData.DB_USER};" +
                           $"Password={dbConnectionData.DB_PASSWORD};" +
                           $"Connect Timeout=60;Encrypt=False";
    }
    else
    {
        throw new Exception("No se pudo obtener los datos de conexión desde Secrets Manager.");
    }
}
else
{
    string dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
    string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "AccesoAlimentario";
    string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "dev";
    string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Lpa1234$";
    connectionString = $"Data Source={dbServer};Initial Catalog={dbName};User ID={dbUser};Password={dbPassword};Connect Timeout=60;Encrypt=False";
}


builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options
        .UseSqlServer(connectionString, x =>
        {
            x.MigrationsAssembly("AccesoAlimentario.Core");
            x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        })
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseCors(corsDevelop);

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
);

app.MapControllers();

app.Run();
