using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations;
using AccesoAlimentario.Web.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger
builder.Services.AddSwaggerService();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options
        .UseSqlServer("Data Source=192.168.1.51;Initial Catalog=AccesoAlimentario;User ID=dev;Password=Lpa1234$;Connect Timeout=60;Encrypt=False", x =>
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