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
public class HeladerasController(ISender sender) : ControllerBase
{
    [HttpDelete]
    public async Task<IResult> Delete([FromBody] BajaHeladera.BajaHeladeraCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpGet("{id}")]
    public async Task<IResult> Get(Guid id)
    {
        return await sender.Send(new ConsultarEstadoHeladera.ConsultarEstadoHeladeraCommand { Id = id });
    }
    
    [HttpPut]
    public async Task<IResult> Put([FromBody] ModificacionHeladera.ModificacionHeladeraCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost]
    public async Task<IResult> Post([FromBody] RegistrarAperturaHeladera.RegistrarAperturaHeladeraCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("RetirarVianda")]
    public async Task<IResult> RetirarVianda([FromBody] RetirarVianda.RetirarViandaCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("SolicitarAutorizacionAperturaDeHeladera")]
    public async Task<IResult> SolicitarAutorizacionAperturaDeHeladera([FromBody] SolicitarAutorizacionAperturaDeHeladera.SolicitarAutorizacionAperturaDeHeladeraCommand command)
    {
        return await sender.Send(command);
    }
}