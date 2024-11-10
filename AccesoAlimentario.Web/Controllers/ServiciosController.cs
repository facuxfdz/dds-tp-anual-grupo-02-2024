﻿using AccesoAlimentario.Operations.Externos;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Servicios")]
[ApiController]
public class ServiciosController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Obtiene colaboradores para reconocimiento basados en los criterios de puntos mínimos y donaciones de viandas.
    /// </summary>
    /// <param name="command">Parámetros de filtrado para obtener los colaboradores.</param>
    /// <returns>Una lista de colaboradores que cumplen los criterios de puntos y donaciones.</returns>
    /// <response code="200">Retorna la lista de colaboradores válidos para reconocimiento.</response>
    /// <response code="400">Si los parámetros de entrada son inválidos.</response>
    [HttpGet("ObtenerColaboraderesParaReconocimiento")]
    [SwaggerOperation(
        Summary = "Obtener colaboradores para reconocimiento",
        Description = "Este endpoint obtiene una lista de colaboradores que cumplen con los requisitos de puntos mínimos y donaciones de viandas en el último mes."
    )]
    [ProducesResponseType(typeof(List<ColaboradorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ObtenerColaboraderesParaReconocimiento.ObtenerColaboraderesParaReconocimientoCommand command)
    {
        return await sender.Send(command);
    }
    
    [HttpGet("ObtenerRecomendacionUbicacionHeladera")]
    public async Task<IResult> ObtenerRecomendacionUbicacionHeladera([FromQuery] ObtenerRecomendacionUbicacionHeladera.ObtenerRecomendacionUbicacionHeladeraCommand command)
    {
        return await sender.Send(command);
    }
}