using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Heladeras;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Heladeras;

public class TestModificacionHeladera
{
    [Test]
    
    // TODO: NO FUNCIONA

    public async Task ModificacionHeladeraTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var heladera = context.Heladeras.First();
        var puntoEstrategicoRequest = MockRequest.GetPuntoEstrategicoRequest();
        var sensorRequest = MockRequest.GetSensorTemperaturaRequest();

        var command = new ModificacionHeladera.ModificacionHeladeraCommand()
        {
            Id = heladera.Id,
            Estado = EstadoHeladera.Activa,
            PuntoEstrategico = puntoEstrategicoRequest ,
            TemperaturaMinimaConfig = 14,
            TemperaturaMaximaConfig = 22,
            Sensores = [sensorRequest]

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
                Assert.Pass($"El comando devolvió Ok. Se pudo modificar la heladera de Id: {heladera.Id}" );
                break;
            default:
                Assert.Fail($"El comando no devolvió ok - {result.GetType()}"); 
                break;
        }
    }
}