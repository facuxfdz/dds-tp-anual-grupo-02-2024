﻿using AccesoAlimentario.Operations.Roles.Colaboradores;
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
    
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            return await sender.Send(new ObtenerColaboradores.ObtenerAllCommand());
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
            return await sender.Send(new ObtenerPuntajeColaborador.ObtenerPuntajeColaboradorCommand { ColaboradorId = id });
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

    [HttpPost("SuscibirseHeladera")]
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
}