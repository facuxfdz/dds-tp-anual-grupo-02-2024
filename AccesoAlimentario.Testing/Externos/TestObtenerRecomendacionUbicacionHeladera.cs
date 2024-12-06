using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Externos;
using AccesoAlimentario.Testing.Utils;

namespace AccesoAlimentario.Testing.Externos;

public class TestObtenerRecomendacionUbicacionHeladera
{
    [Test]
    public async Task ObtenerRecomendacionUbicacionHeladera()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        var command = new ObtenerRecomendacionUbicacionHeladera.ObtenerRecomendacionUbicacionHeladeraCommand
        {
            Latitud = 0,
            Longitud = 0,
            Radio = 0
        };
        
        var result = await mediator.Send(command);
        
        
        // Respuesta no esperada para valores incorrectos
        var badResult = result as Microsoft.AspNetCore.Http.HttpResults.BadRequest;
        
        // Respuesta de un recurso no encontrado
        var notFoundResult = result as Microsoft.AspNetCore.Http.HttpResults.NotFound;
        
        // Respuesta esperada
        var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok<List<PuntoEstrategico>>;
        
        // Procesar los datos obtenidos
        okResult!.Value!.ForEach(punto =>
        {
            Console.WriteLine(punto.Id);
            Console.WriteLine(punto.Nombre);
            Console.WriteLine(punto.Latitud);
            Console.WriteLine(punto.Longitud);
            Console.WriteLine(punto.Direccion.Calle);
            Console.WriteLine(punto.Direccion.Numero);
            Console.WriteLine(punto.Direccion.Localidad);
            Console.WriteLine(punto.Direccion.CodigoPostal);
            Console.WriteLine(punto.Direccion.Piso);
            Console.WriteLine(punto.Direccion.Departamento);
        });
        
        Assert.Pass();
    }
}