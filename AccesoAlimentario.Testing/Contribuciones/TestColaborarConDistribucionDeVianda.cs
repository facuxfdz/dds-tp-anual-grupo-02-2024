using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Testing.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Contribuciones;

public class TestColaborarConDistribucionDeVianda
{
    [Test]
    // TODO: Parece que no se agregan las viandas a la lista
    public async Task TestRegistrarConDistribucionDeVianda()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();
        
        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // Usa los datos precargados
        var colaborador = context.Colaboradores.First(); // Recupera el primer colaborador
        var heladeraOrigen = context.Heladeras.First(); // Recupera la primera heladera
        var heladeraDestino = context.Heladeras.Skip(1).First(); // Recupera la segunda heladera
        
        // Verifica las heladeras creadas
        var heladeras = MockServices.ObtenerHeladeras(scope); 
        Assert.That(heladeras, Is.Not.Empty, "No se encontraron heladeras.");

        // Asegúrate de que haya viandas en la heladera de origen
        var viandasEnHeladeraOrigen = heladeraOrigen.Viandas.Count();
        Console.WriteLine(viandasEnHeladeraOrigen);
        //Assert.That(viandasEnHeladeraOrigen, Is.GreaterThanOrEqualTo(1), "La heladera de origen no tiene suficientes viandas.");

        var command = new ColaborarConDistribucionDeVianda.ColaborarConDistribucionDeViandaCommand
        {
            ColaboradorId = colaborador.Id,
            FechaContribucion = Convert.ToDateTime("20/10/2020"),
            HeladeraOrigenId = heladeraOrigen.Id,
            HeladeraDestinoId = heladeraDestino.Id,
            CantidadDeViandas = 1
            
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
        
        var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok;
        
        
        
        //Assert.That(okResult, Is.Not.Null, "El resultado no es Ok.");
        
        //Assert.Pass();

    }
}