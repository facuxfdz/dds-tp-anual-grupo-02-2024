using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConOfertaDePremio
{
    [Test]

    public async Task TestOfertaDePremio()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var colaborador = context.Colaboradores.First();
        var premio = context.Premios.First();

        var command = new ColaborarConOfertaDePremio.ColaborarConOfertaDePremioCommand
        {
            ColaboradorId = colaborador.Id,
            FechaContribucion = DateTime.Now,
            Nombre = premio.Nombre,
            PuntosNecesarios = premio.PuntosNecesarios,
            Imagen = premio.Imagen,
            Rubro = premio.Rubro

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
                Assert.Pass("El comando devolvió Ok.");
                break;
            default:
                Assert.Fail($"El comando no devolvió ok - {result.GetType()}"); 
                break;
        }
    }
}