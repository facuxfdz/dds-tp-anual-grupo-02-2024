using AccesoAlimentario.Operations.Externos;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Servicios")]
[ApiController]
public class ServiciosController(ISender sender) : ControllerBase
{
    [HttpGet("ObtenerColaboraderesParaReconocimiento")]
    public async Task<IResult> Get([FromQuery] ObtenerColaboraderesParaReconocimiento.ObtenerColaboraderesParaReconocimientoCommand command)
    {
        return await sender.Send(command);
    }
}