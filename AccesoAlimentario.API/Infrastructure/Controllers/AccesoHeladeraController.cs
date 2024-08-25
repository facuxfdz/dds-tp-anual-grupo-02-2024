using AccesoAlimentario.API.Controllers.RequestDTO.AccesoHeladera;
using AccesoAlimentario.API.UseCases.AccesoHeladera;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccesoHeladeraController(AutorizarAccesoHeladera autorizarAccesoHeladera) : ControllerBase
{
    // POST: api/accesoHeladera/registrarAutorizacion
    [HttpPost("registrarAutorizacion")]
    public IActionResult RegistrarAutorizacion([FromBody] AutorizacionDTO autorizacion)
    {
        try
        {
            autorizarAccesoHeladera.CrearAutorizacion(autorizacion);
        }
        catch (ColaboradorNoExiste e)
        {
            return NotFound(e.Message);
        }
        catch (TarjetaInexistente e)
        {
            return NotFound(e.Message);
        }
        catch (HeladeraNoExiste e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            // Server error
            return StatusCode(500, e.Message);
        }
        return Ok();
    }
}