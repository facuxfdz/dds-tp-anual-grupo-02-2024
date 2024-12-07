using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Tecnicos;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Tecnicos;

public class TesrRegistrarVisitaTecnica
{
    [Test]

    public async Task RegistrarVisitaTecnicaTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var tecnico = context.Roles.OfType<Tecnico>().Last();
        var incidenteFalla = context.Incidentes.OfType<FallaTecnica>().First();

        var command = new RegistrarVisitaHeladera.RegistrarVisitaHeladeraCommand
        {
            Comentario = "Resuelto",
            Fecha = DateTime.UtcNow,
            IncidenteId = incidenteFalla.Id,
            Resuelto = true,
            TecnicoId = tecnico.Id
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
                Assert.Pass($"El comando registró la visita tecnica para el incidente {incidenteFalla.Id}");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}