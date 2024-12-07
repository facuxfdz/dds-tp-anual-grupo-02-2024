using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Tecnicos;

namespace AccesoAlimentario.Testing.Tecnicos;

public class TestBajaTecnico
{
    [Test]
    
    // TODO: Cuando lo corro solo funcan pero si corro todo junto me tiran error 
    
    public async Task BajaTecnicoTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var tecnico = context.Roles.OfType<Tecnico>().First();

        var command = new BajaTecnico.BajaTecnicoCommand
        {
            Id = tecnico.Id,
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
                Assert.Pass($"El comando dio de baja al tecnico: {tecnico.Id}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}