using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Colaboradores")]
[ApiController]
public class ColaboradoresController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Post([FromBody] AltaColaborador.AltaColaboradorCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        return await sender.Send(new BajaColaborador.BajaColaboradorCommand { Id = id });
    }
    
    [HttpPost("importar/csv")]
    public async Task<IResult> ImportarCsv([FromBody] ImportarColaboradoresCsv.ImportarColaboradoresCsvCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpGet("{id}/puntaje")]
    public async Task<IResult> GetPuntaje(Guid id)
    {
        return await sender.Send(new ObtenerPuntajeColaborador.ObtenerPuntajeColaboradorCommand { ColaboradorId = id });
    }
    
    [HttpPost("reportar/fallaTecnica")]
    public async Task<IResult> ReportarFallaTecnica([FromBody] ReportarFallaTecnica.ReportarFallaTecnicaCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("SuscibirseHeladera")]
    public async Task<IResult> SuscribirseHeladera([FromBody] SuscribirseHeladera.SuscribirseHeladeraCommand command)
    {
        return await sender.Send(command);
    }
}