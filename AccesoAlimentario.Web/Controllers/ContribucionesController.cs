using AccesoAlimentario.Operations.Contribuciones;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Contribuciones")]
[ApiController]
public class ContribucionesController(ISender sender) : ControllerBase
{
    [HttpPost("DistribucionVianda")]
    public async Task<IResult> DistribucionVianda([FromBody] ColaborarConDistribucionDeVianda.ColaborarConDistribucionDeViandaCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("DonacionHeladera")]
    public async Task<IResult> DonacionHeladera([FromBody] ColaborarConDonacionDeHeladera.ColaborarConDonacionDeHeladeraCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("DonacionVianda")]
    public async Task<IResult> DonacionVianda([FromBody] ColaborarConDonacionDeVianda.ColaborarConDonacionDeViandaCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("DonacionMonetaria")]
    public async Task<IResult> DonacionMonetaria([FromBody] ColaborarConDonacionMonetaria.ColaborarConDonacionMonetariaCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("OfertaPremio")]
    public async Task<IResult> OfertaPremio([FromBody] ColaborarConOfertaDePremio.ColaborarConOfertaDePremioCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("RegistroPersonaVulnerable")]
    public async Task<IResult> RegistroPersonaVulnerable([FromBody] ColaborarConRegistroPersonaVulnerable.ColaborarConRegistroPersonaVulnerableCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpPost("CanjeDePremio")]
    public async Task<IResult> CanjeDePremio([FromBody] RegistrarCanjeDePremio.RegistrarCanjeDePremioCommand command)
    {
        return await sender.Send(command);
    }
}