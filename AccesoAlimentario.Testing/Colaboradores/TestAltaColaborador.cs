using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestAltaColaborador
{
    [Test]

    public async Task AltaColaboradorTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var direccionRequest = MockRequest.GetDireccionRequest();
        var documentoIdentidad = MockRequest.GetDocumentoIdentidadRequest();
        var personaHumanaRequest = MockRequest.GetPersonaHumanaRequest();
        var medioContactoEmailRequest = MockRequest.GetEmailRequest();


        var command = new AltaColaborador.AltaColaboradorCommand
        {
            ContribucionesPreferidas = [],
            Direccion = direccionRequest,
            Documento = documentoIdentidad,
            MediosDeContacto = [medioContactoEmailRequest],
            Password = "wfejh89453hkjhkfd",
            Persona = personaHumanaRequest,
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
                Assert.Pass($"El comando devolvió el alta del colaborador cuyo id es: {okResult.Value}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}