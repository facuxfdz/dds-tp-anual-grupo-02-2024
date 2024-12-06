using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Heladeras;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Heladeras;

public class TestConsultarEstadoHeladera
{
    [Test]

    public async Task TestConsultaEstadoHeladera()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var heladera = context.Heladeras.First();

        var command = new ConsultarEstadoHeladera.ConsultarEstadoHeladeraCommand
        {
            Id = heladera.Id
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok<EstadoHeladera> okResult:
                // Accede al valor dentro del Ok
                var registro = okResult.Value;
                if (registro != null)
                {
                    Console.WriteLine($"Id de la heladera: {heladera.Id} \nEstado: {registro.ToString()}");
                }
                Assert.Pass("El comando devolvió la donacion de la heladera.");
                break;
            default:
                Assert.Fail($"El comando no devolvió nulo - {result.GetType()}"); 
                break;
        }

    }
}