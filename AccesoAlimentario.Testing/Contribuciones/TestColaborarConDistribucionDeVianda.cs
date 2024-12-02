using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConDistribucionDeVianda
{
    [Test]
    public async Task TestRegistrarConDistribucionDeVianda()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Usa los datos precargados
        var colaborador = context.Roles.OfType<Colaborador>().First(); // Recupera el primer colaborador
        var heladeraOrigen = context.Heladeras.First(); // Recupera la primera heladera
        var heladeraDestino = context.Heladeras.Skip(1).First(); // Recupera la segunda heladera
        

        var command = new ColaborarConDistribucionDeVianda.ColaborarConDistribucionDeViandaCommand
        {
            ColaboradorId = colaborador.Id,
            FechaContribucion = DateTime.Now,
            HeladeraOrigenId = heladeraOrigen.Id,
            HeladeraDestinoId = heladeraDestino.Id,
            CantidadDeViandas = 1
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
                Assert.Pass("El comando devolvió Ok. Se distribuyó la vianda. ");
                break;
            default:
                Assert.Fail($"El comando no devolvió ok - {result.GetType()}"); 
                break;
        }
    }
}