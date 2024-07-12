using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonasController(PersonasServicio servicio) : ControllerBase
{
    // GET: api/personas
    [HttpGet]
    public IActionResult GetPersonas()
    {
        var personas = servicio.Obtener();
        return Ok(personas);
    }
    
    // POST: api/personas/humana
    [HttpPost("humana")]
    public IActionResult AddPersonaHumana(
        [FromBody] PersonaHumana personaHumana
    )
    {
        var p = servicio.CrearHumana(personaHumana.Nombre, personaHumana.Direccion, personaHumana.DocumentoIdentidad,
            new List<MedioContacto>(), personaHumana.Apellido, personaHumana.Sexo);
        return Ok(p);
    }
    
    // POST: api/personas/juridica
    [HttpPost("juridica")]
    public IActionResult AddPersonaJuridica(
        [FromBody] PersonaJuridica personaJuridica
    )
    {
        var p = servicio.CrearJuridica(personaJuridica.Nombre, personaJuridica.Direccion, personaJuridica.DocumentoIdentidad,
            new List<MedioContacto>(), personaJuridica.Tipo, personaJuridica.Rubro);
        return Ok(p);
    }
    
    // DELETE: api/personas/5
    [HttpDelete("{id}")]
    public IActionResult DeletePersona(int id)
    {
        servicio.Eliminar(id);
        return Ok();
    }
}