using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Colaboradores;

public class TestObtenerPuntajeColaborador
{
    [Test]

    public async Task ObtenerPuntajesColaboradorTest()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var colaborador = context.Roles.OfType<Colaborador>().First();


        var command = new ObtenerPuntajeColaborador.ObtenerPuntajeColaboradorCommand
        {
            
            ColaboradorId = colaborador.Id,
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
            case Microsoft.AspNetCore.Http.HttpResults.Ok<float> okResult:
                Assert.Pass($"El puntaje del colaborador cuyo id es: {colaborador.Id} es de: {okResult.Value} puntos. \n");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
    }
}