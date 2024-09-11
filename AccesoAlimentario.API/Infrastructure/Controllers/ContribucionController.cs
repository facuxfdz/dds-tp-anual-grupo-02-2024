using AccesoAlimentario.API.UseCases.Colaboraciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Contribuciones;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContribucionController(
    CrearContribucion crearContribucion
) : ControllerBase
{
    [HttpPost]
    public IActionResult PostColaboracion([FromBody] ContribucionDTO colaboracion)
    {
        try
        {
            crearContribucion.Crear(colaboracion);
        }
        catch (Exception e)
        {
            // Server error
            return StatusCode(500, e.Message);
        }

        return Ok();
    }
}