using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;


namespace AccesoAlimentario.Testing.Csv;

public class TestImportarColaboradoresCsv
{
    [Test]
    
    //TODO: No funciona 

    public async Task ImportarColaboradoresCsv()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var archivo = MockServices.CrearArchivoCsvColaboradores();

        var command = new ImportarColaboradoresCsv.ImportarColaboradoresCsvCommand
        {
            Archivo = archivo

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
                Assert.Pass($"El comando importó a los colaboradores.");
                break;
            default:
                Assert.Fail($"El comando devolvió un tipo inesperado - {result.GetType()}");
                break;
        }
        
    }
}