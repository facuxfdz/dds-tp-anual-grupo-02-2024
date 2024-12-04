using AccesoAlimentario.Operations.Sensores;
using AccesoAlimentario.Operations.Sensores.Movimiento;
using AccesoAlimentario.Operations.Sensores.Temperatura;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Sensores")]
[ApiController]
public class SensoresController(ISender sender, ILogger<SensoresController> logger) : ControllerBase
{
    [HttpPost("movimiento")]
    public async Task<IResult> Post([FromBody] AltaRegistroMovimiento.AltaRegistroMovimientoCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de alta un registro de movimiento");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("temperatura")]
    public async Task<IResult> Post([FromBody] AltaRegistroTemperatura.AltaRegistroTemperaturaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de alta un registro de temperatura");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(Guid id)
    {
        try
        {
            return await sender.Send(new ObtenerSensor.ObtenerSensorCommand { SensorId = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener un sensor");
            return Results.StatusCode(500);
        }
    }
}