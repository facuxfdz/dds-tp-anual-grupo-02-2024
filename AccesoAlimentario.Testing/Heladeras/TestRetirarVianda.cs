﻿using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations.Heladeras;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Heladeras;

public class TestRetirarVianda
{
    
    //TODO: Parece que funciona pero tengo dudas
    [Test]

    public async Task RetirarViandaTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var tarjetaConsumo = context.Tarjetas.OfType<TarjetaConsumo>().First();
        var heladera = context.Heladeras.First();

        var command = new RetirarVianda.RetirarViandaCommand
        {
            HeladeraId = heladera.Id,
            TarjetaId = tarjetaConsumo.Id
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok<Vianda> okResult:
                // Accede al valor dentro del Ok
                var registro = okResult.Value;
                if (registro != null)
                {
                        Console.WriteLine($"Id de vianda retirada:" +
                                          $" {registro.Id}");
                }
                Assert.Pass("El comando devolvió el registro de la persona vulnerable.");
                break;
           // default:
             //   Assert.Fail($"El comando no devolvió nulo - {result.GetType()}"); 
               // break;
        }

    }
}