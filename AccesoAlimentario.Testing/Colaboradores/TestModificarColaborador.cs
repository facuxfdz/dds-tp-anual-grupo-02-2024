using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestModificarColaborador
{
    [Test]

    public async Task ModificarColaboradorTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var personaHumanaRequest = MockRequest.GetPersonaHumanaRequest();
        var medioContactoTelefonoRequest = MockRequest.GetTelefonoRequest();
        var colaborador = context.Roles.OfType<Colaborador>().First();

        var command = new ModificacionColaborador.ModificacionColaboradorCommand
        {
            Id = colaborador.Id,
            Persona = personaHumanaRequest,
            MediosDeContacto = [medioContactoTelefonoRequest]
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
                Assert.Pass($"El comando modificó al colaborador cuyo id es: {colaborador.Id}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}