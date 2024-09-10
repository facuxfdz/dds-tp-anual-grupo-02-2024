using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Incidentes;
using AccesoAlimentario.API.UseCases.Incidentes.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Incentes;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncidentesController(CrearFallaTecnica crearFallaTecnica, CrearVisitaTecnica crearVisitaTecnica) : ControllerBase
{
    [HttpPost]
    public IActionResult AddIncidente(
        [FromBody] FallaTecnicaDTO fallaTecnica
    )
    {
        try
        {
            crearFallaTecnica.Crear(fallaTecnica);
        }
        catch (HeladeraNoExiste e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (ColaboradorNoExiste e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }

        return Ok(new { message = "Incidente creado correctamente" });
    }
    
    [HttpPost]
    public IActionResult AddVisitaTecnica(
        [FromBody] VisitaTecnicaDTO visitaTecnica
    )
    {
        try
        {
            crearVisitaTecnica.Crear(visitaTecnica);
        }
        catch (HeladeraNoExiste e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (TecnicoNoExiste e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }

        return Ok(new { message = "Incidente creado correctamente" });
    }
}