using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Tecnicos;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Tecnicos;

public class TestModificacarTecnico
{
    [Test]
    
    //TODO: NO FUNCIONA

    public async Task ModificarTecnicoTest()
    {
        
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var personaHumanaRequest = MockRequest.GetPersonaHumanaRequest();
        var medioContactoTelefonoRequest = MockRequest.GetTelefonoRequest();
        var tecnico = context.Roles.OfType<Tecnico>().Last();

        var command = new ModificacionTecnico.ModificacionTecnicoCommand
        {
            Id = tecnico.Id,
            AreaCoberturaLongitud = 2,
            AreaCoberturaLatitud = 3,
            AreaCoberturaRadio = 4,
            Persona = personaHumanaRequest,
            MediosDeContacto = [medioContactoTelefonoRequest],
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
                Assert.Pass($"El comando modificó al tecnico cuyo id es: {tecnico.Id}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}