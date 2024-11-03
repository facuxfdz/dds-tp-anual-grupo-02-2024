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
public class SensoresController(ISender sender) : ControllerBase
{
    [HttpPost("movimiento")]
    public async Task<IResult> Post([FromBody] AltaRegistroMovimiento.AltaRegistroMovimientoCommand command)
    {
        return await sender.Send(command);
    }

    [HttpPost("temperatura")]
    public async Task<IResult> Post([FromBody] AltaRegistroTemperatura.AltaRegistroTemperaturaCommand command)
    {
        return await sender.Send(command);
    }
}