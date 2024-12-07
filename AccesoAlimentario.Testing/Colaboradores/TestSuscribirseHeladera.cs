using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestSuscribirseHeladera
{
    [Test]

    public async Task SuscribirseHeladeraTest()
    {
        
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var colaborador = context.Roles.OfType<Colaborador>().First();
        var heladera = context.Heladeras.First();

        var command = new SuscribirseHeladera.SuscribirseHeladeraCommand
        {
            ColaboradorId = colaborador.Id,
            HeladeraId = heladera.Id,
            Tipo = SuscribirseHeladera.SuscribirseHeladeraCommand.TipoSuscripcion.Faltante,
            Minimo = 4,
            Maximo = 10

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
                Assert.Pass($"El comando realizó la suscripcion de colaborador cuyo id es:{colaborador.Id} a la heladera: {heladera.Id}. \n \n");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}