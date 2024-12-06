using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConRegistroPersonaVulnerable
{
    [Test]
    
    public async Task TestRegistrarPersonaVulnerable()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var colaborador = context.Roles.OfType<Colaborador>().First();
        var direccionRequest = MockRequest.GetDireccionRequest();
        var tarjetaConsumoRequest = MockRequest.GetTarjetaConsumoRequest();
        var personaRequest = MockRequest.GetPersonaRequest();
        var documentoIdentidadRequest = MockRequest.GetDocumentoIdentidadRequest();

        var command = new ColaborarConRegistroPersonaVulnerable.ColaborarConRegistroPersonaVulnerableCommand
        {
            ColaboradorId = colaborador.Id,
            Direccion = direccionRequest,
            Tarjeta = tarjetaConsumoRequest,
            Persona = personaRequest,
            Documento = documentoIdentidadRequest,
            MediosDeContacto = [],
            CantidadDeMenores = 0
            
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok okResult:
                // Accede al valor dentro del Ok
                Assert.Pass("El comando devolvió el registro de la persona vulnerable.");
                break;
            default:
                Assert.Fail($"El comando no devolvió nulo - {result.GetType()}"); 
                break;
        }
    }
}