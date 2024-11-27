using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
using AccesoAlimentario.Operations.Externos;
using AccesoAlimentario.Testing.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Externos;

public class TestObtenerColaboradoresParaReconocimiento
{
    [Test]
    public async Task ObtenerColaboradoresParaReconocimiento()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        var command = new ObtenerColaboraderesParaReconocimiento.ObtenerColaboraderesParaReconocimientoCommand
        {
            CantidadDeColaboradores = 5,
            DonacionesViandasMinimas = 0,
            PuntosMinimos = 0,
        };

        var result = await mediator.Send(command);

        var badResult = result as Microsoft.AspNetCore.Http.HttpResults.BadRequest;
        if (badResult != null)
        {
            Assert.Fail("El comando devolvió BadRequest.");
        }

        var notFoundResult = result as Microsoft.AspNetCore.Http.HttpResults.NotFound;
        if (notFoundResult != null)
        {
            Assert.Fail("El comando devolvió NotFound.");
        }
        
        var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok<List<ColaboradorResponse>>;
        
        okResult!.Value!.ForEach(colaborador =>
        {
            Console.WriteLine(colaborador.Id);
            Console.WriteLine(colaborador.Nombre);
            Console.WriteLine(colaborador.DonacionesUltimoMes);
            Console.WriteLine(colaborador.Puntos);
        });

        Assert.Pass();
    }
}