using AccesoAlimentario.Operations.Heladeras;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Heladeras")]
[ApiController]
public class HeladerasController(ISender sender, ILogger<HeladerasController> logger) : ControllerBase
{
    [HttpDelete]
    public async Task<IResult> Delete([FromBody] BajaHeladera.BajaHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de baja una heladera");
            return Results.StatusCode(500);
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetHeladeras()
    {
        try
        {
            return await sender.Send(new ObtenerHeladeras.ObtenerHeladerasQuery());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener las heladeras");
            return Results.StatusCode(500);
        }
    }
    
    [HttpGet("Consultar")]
    public async Task<IResult> GetHeladera([FromQuery] ConsultarHeladera.ConsultarHeladeraQuery query)
    {
        try
        {
            return await sender.Send(query);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al consultar una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}/estado")]
    public async Task<IResult> Get(Guid id)
    {
        try
        {
            return await sender.Send(new ConsultarEstadoHeladera.ConsultarEstadoHeladeraCommand { Id = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al consultar el estado de una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IResult> Put([FromBody] ModificacionHeladera.ModificacionHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al modificar una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] RegistrarAperturaHeladera.RegistrarAperturaHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al registrar la apertura de una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("RetirarVianda")]
    public async Task<IResult> RetirarVianda([FromBody] RetirarVianda.RetirarViandaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al retirar una vianda");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("SolicitarAutorizacionAperturaDeHeladera")]
    public async Task<IResult> SolicitarAutorizacionAperturaDeHeladera(
        [FromBody] SolicitarAutorizacionAperturaDeHeladera.SolicitarAutorizacionAperturaDeHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al solicitar autorización para la apertura de una heladera");
            return Results.StatusCode(500);
        }
    }
}