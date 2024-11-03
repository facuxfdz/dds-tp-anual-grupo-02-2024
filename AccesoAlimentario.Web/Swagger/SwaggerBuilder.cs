using AccesoAlimentario.Web.Constants;
using Microsoft.OpenApi.Models;

namespace AccesoAlimentario.Web.Swagger;

public static class SwaggerBuilder
{
    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.SwaggerDoc(ApiConstants.AccesoAlimentarioName,
                    new OpenApiInfo
                    {
                        Title = "AccesoAlimentario API",
                        Version = "v1",
                    });
            }
        );
    }

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/" + ApiConstants.AccesoAlimentarioName + "/swagger.json", "AccesoAlimentario API");
        });
    }
}