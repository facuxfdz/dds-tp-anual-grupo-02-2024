using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestBajaColaborador
{
    [Test]

    public async Task BajaColaboradorTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var colaborador = context.Roles.OfType<Colaborador>().First();
        
        var command = new BajaColaborador.BajaColaboradorCommand
        {
            Id = colaborador.Id,
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
                Assert.Pass($"El comando dio de baja al colaborador: {colaborador.Id}.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
        
    }
}