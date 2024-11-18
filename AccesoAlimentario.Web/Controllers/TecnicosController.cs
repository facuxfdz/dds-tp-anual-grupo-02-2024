using AccesoAlimentario.Operations.Roles.Tecnicos;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Tecnicos")]
[ApiController]
public class TecnicosController(ISender sender, ILogger<TecnicosController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Post([FromBody] AltaTecnico.AltaTecnicoCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de alta un técnico");
            return Results.StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        try
        {
            return await sender.Send(new BajaTecnico.BajaTecnicoCommand { Id = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de baja un técnico");
            return Results.StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IResult> Put([FromBody] ModificacionTecnico.ModificacionTecnicoCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al modificar un técnico");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("registrar/visita")]
    public async Task<IResult> Post([FromBody] RegistrarVisitaHeladera.RegistrarVisitaHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al registrar una visita a una heladera");
            return Results.StatusCode(500);
        }
    }
}