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
        
        //TODO: DEBERIA SER LISTA DE COLABORADORES REPONSE
        var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok<List<Colaborador>>;
        
        using var scope = mockServices.GetScope();

// Obtén el contexto desde el scope
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

// Consulta los colaboradores almacenados en la base de datos
        var colaboradores = context.Colaboradores.Include(rol => rol.Persona)
            .ThenInclude(persona => persona.DocumentoIdentidad).ToList();

// Mostrar los datos en la consola para verificar lo que hay en la base de datos
        foreach (var colaborador in colaboradores)
        {
            if (colaborador.Persona.DocumentoIdentidad != null)
                Console.WriteLine(
                    $"Colaborador: {colaborador.Persona.Nombre}, Documento: {colaborador.Persona.DocumentoIdentidad.NroDocumento}, Puntos: {colaborador.Puntos}");
        }

/* okResult!.Value!.ForEach(colaborador =>
 {
     Console.WriteLine(colaborador.Persona.Nombre);
     if (colaborador.Persona.DocumentoIdentidad != null)
         Console.WriteLine(colaborador.Persona.DocumentoIdentidad.Id);
     Console.WriteLine(colaborador.Puntos);

 });*/

 Assert.Pass();

}



}