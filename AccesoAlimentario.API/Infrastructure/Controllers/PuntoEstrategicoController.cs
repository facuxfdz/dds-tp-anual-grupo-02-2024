using AccesoAlimentario.API.UseCases;
using AccesoAlimentario.API.UseCases.Heladeras;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PuntoEstrategicoController(
    DarAltaPuntoHeladera darAltaPuntoHeladera) : ControllerBase
{
    // POST api/puntoestrategico
    [HttpPost]
    public IActionResult AddPuntoEstrategico(
        [FromBody] PuntoEstrategicoDTO puntoEstrategico
    )
    {
        try
        {
            darAltaPuntoHeladera.AltaPunto(puntoEstrategico);
        }
        catch (RequestInvalido e)
        {
            return BadRequest(e.Message);
        }
        catch (RecursoYaExistente e)
        {
            return BadRequest(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok();
    }
    
    // GET api/puntosestrategicos
    [HttpGet]
    public IActionResult GetPuntosEstrategicos()
    {
        throw new NotImplementedException();
    }
}