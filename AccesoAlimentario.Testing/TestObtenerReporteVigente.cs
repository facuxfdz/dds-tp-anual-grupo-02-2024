using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Reportes;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing;

public class TestObtenerReporteVigente
{
    [Test]

    public async Task ObtenerReporteVigenteTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var command = new ObtenerReporteVigente.ObtenerReporteVigenteCommand
        {
            TipoReporte = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA
        };
        
        var result = await mediator.Send(command);
        
        switch (result)
        {
            case Microsoft.AspNetCore.Http.HttpResults.BadRequest<string> badRequest:
                Assert.Fail($"El comando devolvió BadRequest: {badRequest.Value}");
                break;
            case Microsoft.AspNetCore.Http.HttpResults.NotFound<string> notFound:
                Assert.Fail($"El comando devolvió NotFound: {notFound.Value}");
                break;
            case Microsoft.AspNetCore.Http.HttpResults.Ok<Reporte> okResult:
                // Accede al valor dentro del Ok
                var registro = okResult.Value;
                if (registro != null)
                {
                    Console.WriteLine($" El reporte de {registro.Tipo} indica:" +
                                          $" {registro.Cuerpo}");
                }
                Assert.Pass($"El comando devolvió el reporte de: {registro.Tipo}.");
                break;
            default:
                Assert.Fail($"El comando no devolvió nulo - {result.GetType()}"); 
                break;
        }
    }
}