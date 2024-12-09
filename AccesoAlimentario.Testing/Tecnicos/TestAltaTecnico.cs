using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Roles.Tecnicos;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Tecnicos;


public class TestAltaTecnico
{
    [Test]

    public async Task AltaTecnicoTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var direccionRequest = MockRequest.GetDireccionRequest();
        var documentoIdentidad = MockRequest.GetDocumentoIdentidadRequest();
        var personaHumanaRequest = MockRequest.GetPersonaHumanaRequest();
        var medioContactoEmailRequest = MockRequest.GetEmailRequest();

        var command = new AltaTecnico.AltaTecnicoCommand
        {
            AreaCoberturaLongitud = 0,
            AreaCoberturaLatitud = 1,
            AreaCoberturaRadio = 2,
            Persona = personaHumanaRequest,
            Documento = documentoIdentidad,
            MediosDeContacto = [medioContactoEmailRequest]
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok<Guid> okResult:
                Assert.Pass($"El comando devolvió el alta del tecnico cuyo id es: {okResult.Value}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}