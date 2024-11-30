using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConDistribucionDeVianda
{
    [Test]
    // TODO: Parece que no se agregan las viandas a la lista
    public async Task TestRegistrarConDistribucionDeVianda()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Usa los datos precargados
        var colaborador = context.Colaboradores.First(); // Recupera el primer colaborador
        var heladeraOrigen = context.Heladeras.First(); // Recupera la primera heladera
        var heladeraDestino = context.Heladeras.Skip(1).First(); // Recupera la segunda heladera

        // Verifica las heladeras creadas
        var heladeras = MockServices.ObtenerHeladeras(scope);
        Assert.That(heladeras, Is.Not.Empty, "No se encontraron heladeras.");

        // Asegúrate de que haya viandas en la heladera de origen
        var viandasEnHeladeraOrigen = heladeraOrigen.Viandas.Count();
        Console.WriteLine(viandasEnHeladeraOrigen);
        //Assert.That(viandasEnHeladeraOrigen, Is.GreaterThanOrEqualTo(1), "La heladera de origen no tiene suficientes viandas.");

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
                Assert.Pass("El comando devolvió Ok.");
                break;
            default:
                Assert.Fail($"El comando no devolvió ok - {result.GetType()}"); 
                break;
        }
    }
}