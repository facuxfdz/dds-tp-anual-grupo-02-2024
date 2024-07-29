using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PuntoEstrategicoController(
    PuntoEstrategicoServicio puntoEstrategicoServicio, 
    DireccionServicio direccionServicio
    ) : ControllerBase
{
    // POST api/puntoestrategico
    [HttpPost]
    public IActionResult AddPuntoEstrategico(
        [FromBody] PuntoEstrategicoDTO puntoEstrategico
    )
    {
        try
        {
            var direccion = direccionServicio.Buscar(
                puntoEstrategico.Direccion.Calle,
                puntoEstrategico.Direccion.Numero,
                puntoEstrategico.Direccion.Localidad,
                puntoEstrategico.Direccion.CodigoPostal);
            if(direccion == null)
            {
                direccion = direccionServicio.Crear(
                    puntoEstrategico.Direccion.Calle,
                    puntoEstrategico.Direccion.Numero,
                    puntoEstrategico.Direccion.Localidad,
                    puntoEstrategico.Direccion.CodigoPostal
                );
            }
            puntoEstrategicoServicio.Crear(
                puntoEstrategico.Nombre,
                direccion,
                puntoEstrategico.Longitud,
                puntoEstrategico.Latitud
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
        
        return Ok(new { message = "Punto estrategico creado correctamente" });
    }
    
    // GET api/puntosestrategicos
    [HttpGet]
    public IActionResult GetPuntosEstrategicos()
    {
        try
        {
            var puntosEstrategicos = puntoEstrategicoServicio.Listar();
            return Ok(puntosEstrategicos);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }
}