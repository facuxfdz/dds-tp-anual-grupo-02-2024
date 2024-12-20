﻿using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations.Heladeras;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Heladeras;

public class TestSolicitarAutorizacionAperturaDeHeladera
{
    [Test]
    
    public async Task SolicitarAutorizacionAperturaDeHeladeraTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var tarjetaColaboracion = context.Tarjetas.OfType<TarjetaColaboracion>().First();
        var heladera = context.Heladeras.First();
        
        var command = new SolicitarAutorizacionAperturaDeHeladera.SolicitarAutorizacionAperturaDeHeladeraCommand
        {
            HeladeraId = heladera.Id,
            TarjetaId = tarjetaColaboracion.Id
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
                Assert.Pass($"El comando devolvió Ok. Se solicitó autorizacion para apertura" +
                            $" de la heladera con id: {heladera.Id} con la tarjeta con id: {tarjetaColaboracion.Id}");
                break;
            default:
                Assert.Fail($"El comando no devolvió nulo - {result.GetType()}"); 
                break;
        }
        
    }
    
}