using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HeladerasController(HeladerasServicio servicio) : ControllerBase
{
    // POST: api/heladeras
    [HttpPost]
    public IActionResult AddHeladera(
        [FromBody] HeladeraDTO heladera
    )
    {
        try
        {
            servicio.Crear(
                heladera.NombrePuntoEstrategico,
                heladera.DireccionPuntoEstrategico,
                heladera.ModeloId,
                heladera.TemperaturaMinimaConfig,
                heladera.TemperaturaMaximaConfig
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
        
        return Ok(new { message = "Heladera creada correctamente" });
    }
}