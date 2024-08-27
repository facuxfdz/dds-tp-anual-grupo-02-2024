using AccesoAlimentario.API.UseCases.Heladeras;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeladerasController(CrearHeladera crearHeladera, CrearModeloHeladera crearModeloHeladera) : ControllerBase
{
    // POST: api/heladeras
    [HttpPost]
    public IActionResult AddHeladera(
        [FromBody] HeladeraDTO heladera
    )
    {
        try
        {
            crearHeladera.Crear(heladera);
        }
        catch (RequestInvalido e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (PuntoNoExistente e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
        
        return Ok(new { message = "Heladera creada correctamente" });
    }
    
    // POST api/heladeras/modelo
    [HttpPost("modelo")]
    public IActionResult AddModelo(
        [FromBody] ModeloHeladeraDTO modelo
    )
    {
        try
        {
            crearModeloHeladera.Crear(modelo);
        }
        catch (RequestInvalido e)
        {
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
        
        return Ok(new { message = "Modelo de heladera creado correctamente" });
    }
    
}