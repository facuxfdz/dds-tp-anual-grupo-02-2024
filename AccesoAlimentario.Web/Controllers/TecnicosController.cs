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
public class TecnicosController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Post([FromBody] AltaTecnico.AltaTecnicoCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        return await sender.Send(new BajaTecnico.BajaTecnicoCommand { Id = id });
    }
    
    [HttpPut]
    public async Task<IResult> Put([FromBody] ModificacionTecnico.ModificacionTecnicoCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("registrar/visita")]
    public async Task<IResult> Post([FromBody] RegistrarVisitaHeladera.RegistrarVisitaHeladeraCommand command)
    {
        return await sender.Send(command);
    }
}