using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
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
public class ColaboradoresController(ISender sender, ILogger<ColaboradoresController> logger) : ControllerBase
{
    [HttpGet("externos")]
    public async Task<IResult> GetExterno()
    {
        try
        {
            return await sender.Send(new ObtenerColaboradoresExterno.ObtenerColaboradoresExternoCommand());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener los colaboradores para servicios externos");
            return Results.StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            return await sender.Send(new ObtenerColaboradores.ObtenerColaboradoresCommand());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener los colaboradores");
            return Results.StatusCode(500);
        }
    }
    
    [HttpPost]
    public async Task<IResult> Post([FromBody] AltaColaborador.AltaColaboradorCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de alta un colaborador");
            return Results.StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        try
        {
            return await sender.Send(new BajaColaborador.BajaColaboradorCommand { Id = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al dar de baja un colaborador");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("importar/csv")]
    public async Task<IResult> ImportarCsv([FromBody] ImportarColaboradoresCsv.ImportarColaboradoresCsvCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al importar colaboradores desde un archivo CSV");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}/puntaje")]
    public async Task<IResult> GetPuntaje(Guid id)
    {
        try
        {
            return await sender.Send(new ObtenerPuntajeColaborador.ObtenerPuntajeColaboradorCommand
                { ColaboradorId = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener el puntaje de un colaborador");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("reportar/fallaTecnica")]
    public async Task<IResult> ReportarFallaTecnica([FromBody] ReportarFallaTecnica.ReportarFallaTecnicaCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al reportar una falla técnica");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("SuscribirseHeladera")]
    public async Task<IResult> SuscribirseHeladera([FromBody] SuscribirseHeladera.SuscribirseHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al suscribirse a una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpPost("DesuscribirseHeladera")]
    public async Task<IResult> DesuscribirseHeladera(
        [FromBody] DesuscribirseHeladera.DesuscribirseHeladeraCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al desuscribirse de una heladera");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}/suscripciones")]
    public async Task<IResult> GetSuscripciones(Guid id)
    {
        try
        {
            return await sender.Send(new ObtenerSuscripciones.ObtenerSuscripcionesQuery { ColaboradorId = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener las suscripciones de un colaborador");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}/accesos")]
    public async Task<IResult> GetAccesos(Guid id)
    {
        try
        {
            return await sender.Send(new ObtenerAccesos.ObtenerAccesosCommand { ColaboradorId = id });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener los accesos de un colaborador");
            return Results.StatusCode(500);
        }
    }

    [HttpGet("{id}/premiosCanjeados")]
    public async Task<IResult> GetPremiosCanjeados(Guid id, [FromQuery] string? nombre,
        [FromQuery] float? puntosNecesarios, [FromQuery] TipoRubro? rubro)
    {
        try
        {
            return await sender.Send(new ObtenerPremiosCanjeados.ObtenerPremiosCanjeadosCommand
            {
                ColaboradorId = id,
                Nombre = nombre,
                PuntosNecesarios = puntosNecesarios,
                Rubro = rubro
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al obtener los premios canjeados de un colaborador");
            return Results.StatusCode(500);
        }
    }
}