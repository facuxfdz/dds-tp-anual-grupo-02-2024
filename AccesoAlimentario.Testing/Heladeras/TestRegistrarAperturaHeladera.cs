using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Testing.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Testing.Heladeras;

public class TestRegistrarAperturaHeladera
{
    [Test]
    
    //TODO: FALTA

    public async Task RegistrarApertura()
    {
        var mockServices = new MockServices();
        var mediator = mockServices.GetMediator();

        using var scope = mockServices.GetScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var heladera = context.Heladeras.First();
    }
}