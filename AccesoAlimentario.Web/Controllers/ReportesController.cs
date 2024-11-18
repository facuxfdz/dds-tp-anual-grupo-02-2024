using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Reportes;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Reportes")]
[ApiController]
public class ReportesController(ISender sender, ILogger<ReportesController> logger) : ControllerBase
{
    [HttpGet("{tipoReporte}")]
    public async Task<IResult> Get(TipoReporte tipoReporte)
    {
        try
        {
            return await sender.Send(new ObtenerReporteVigente.ObtenerReporteVigenteCommand
                { TipoReporte = tipoReporte });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener un reporte");
            return Results.StatusCode(500);
        }
    }
}