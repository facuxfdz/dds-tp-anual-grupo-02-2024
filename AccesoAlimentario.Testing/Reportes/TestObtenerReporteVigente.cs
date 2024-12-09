using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Dto.Responses.Reportes;
using AccesoAlimentario.Operations.Reportes;
using AccesoAlimentario.Testing.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Reportes;

public class TestObtenerReporteVigente
{
    [Test]

    //TODO: No funciona parece que okResult no es de tipo ReporteResponse
    
    public async Task ObtenerReporteVigenteTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var command = new ObtenerReporteVigente.ObtenerReporteVigenteCommand
        {
            TipoReporte = TipoReporte.CANTIDAD_VIANDAS_POR_COLABORADOR
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok<object> okResult:
                var registro = okResult.Value;
                if (registro != null)
                {
                    Assert.Pass($"El comando devolvió el reporte de tipo");
                }
                break;
            default:
                Assert.Fail($"Resultado inesperado del comando: {result.GetType().FullName}");
                break;
        }
    }
}