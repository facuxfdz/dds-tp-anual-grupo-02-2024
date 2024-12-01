using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConDonacionDeVianda
{
    [Test]

    public async Task TestDonacionDeVianda()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var colaborador = context.Colaboradores.First(); // Recupera el primer colaborador
        var heladera = context.Heladeras.First(); // Recupera la primera heladera

        var command = new ColaborarConDonacionDeVianda.ColaborarConDonacionDeViandaCommand
        {
            ColaboradorId = colaborador.Id,
            FechaContribucion = DateTime.Now, 
            HeladeraId = heladera.Id,
            Comida = "Milanesa",
            FechaCaducidad = DateTime.Now,
            Calorias = 100,
            Peso = 200,
            EstadoVianda = EstadoVianda.Disponible

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