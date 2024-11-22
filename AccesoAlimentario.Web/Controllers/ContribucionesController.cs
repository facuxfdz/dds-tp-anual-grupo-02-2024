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
public class ContribucionesController(ISender sender, ILogger<ContribucionesController> logger) : ControllerBase
{
    [HttpPost("DistribucionViandas")]
    public async Task<IResult> DistribucionVianda(
        [FromBody] ColaborarConDistribucionDeVianda.ColaborarConDistribucionDeViandaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con la distribución de viandas");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("DonacionHeladera")]
    public async Task<IResult> DonacionHeladera(
        [FromBody] ColaborarConDonacionDeHeladera.ColaborarConDonacionDeHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con la donación de heladeras");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("DonacionVianda")]
    public async Task<IResult> DonacionVianda(
        [FromBody] ColaborarConDonacionDeVianda.ColaborarConDonacionDeViandaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con la donación de viandas");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("DonacionMonetaria")]
    public async Task<IResult> DonacionMonetaria(
        [FromBody] ColaborarConDonacionMonetaria.ColaborarConDonacionMonetariaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con la donación monetaria");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("OfertaPremio")]
    public async Task<IResult> OfertaPremio(
        [FromBody] ColaborarConOfertaDePremio.ColaborarConOfertaDePremioCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con la oferta de premios");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("RegistroPersonaVulnerable")]
    public async Task<IResult> RegistroPersonaVulnerable(
        [FromBody] ColaborarConRegistroPersonaVulnerable.ColaborarConRegistroPersonaVulnerableCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al colaborar con el registro de personas vulnerables");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("CanjeDePremio")]
    public async Task<IResult> CanjeDePremio([FromBody] RegistrarCanjeDePremio.RegistrarCanjeDePremioCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al registrar el canje de premios");
            return Results.StatusCode(500);
        }
    }
}