using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestReportarFallaTecnica
{
    [Test]

    public async Task ReportarFallaTecnicaTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var heladera = context.Heladeras.First();
        var colaborador = context.Roles.OfType<Colaborador>().First();


        var command = new ReportarFallaTecnica.ReportarFallaTecnicaCommand
        {
            Descripcion = "Se rompió la puerta",
            Fecha = DateTime.UtcNow,
            HeladeraId = heladera.Id,
            ReporteroId = colaborador.Id
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok:
                Assert.Pass($"Se reportó la falla técnica de la heladera: {heladera.Id}. \n \n");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}