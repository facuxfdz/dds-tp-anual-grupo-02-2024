using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.UseCases.Personas;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonasController(CrearPersona crearPersona) : ControllerBase
{
    private void _validarPersona(PersonaDTO persona)
    {
        if(persona.TipoPersona == null)
        {
            Console.WriteLine(persona.TipoPersona);
            Console.WriteLine(persona.Sexo);
            throw new RequestInvalido("Tipo de persona no puede ser nulo");
        }
        if(persona.Nombre == null)
        {
            throw new RequestInvalido("Nombre no puede ser nulo");
        }
        if(persona.Direccion == null)
        {
            throw new RequestInvalido("Direccion no puede ser nulo");
        }
        if(persona.Direccion.Numero == null || persona.Direccion.CodigoPostal == null || persona.Direccion.Localidad == null || persona.Direccion.Calle == null)
        {
            throw new RequestInvalido("Direccion no puede tener campos nulos");
        }
        if(persona.DocumentoIdentidad == null)
        {
            throw new RequestInvalido("Documento de identidad no puede ser nulo");
        }
        if(persona.DocumentoIdentidad.Tipo == null)
        {
            throw new RequestInvalido("Tipo de documento de identidad no puede ser nulo");
        }
    }
    [HttpPost]
    // POST: api/personas
    public ActionResult<PersonaDTO> PostPersona([FromBody] PersonaDTO persona)
    {
        try
        {
            _validarPersona(persona);
            crearPersona.Crear(persona);
        }catch (RequestInvalido e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}
// public class PersonasController(PersonasServicio servicio) : ControllerBase
// {
//     // GET: api/personas
//     [HttpGet]
//     public IActionResult GetPersonas()
//     {
//         var personas = servicio.Obtener();
//         return Ok(personas);
//     }
//     
//     // POST: api/personas/humana
//     [HttpPost("humana")]
//     public IActionResult AddPersonaHumana(
//         [FromBody] PersonaHumana personaHumana
//     )
//     {
//         var p = servicio.CrearHumana(personaHumana.Nombre, personaHumana.Direccion, personaHumana.DocumentoIdentidad,
//             new List<MedioContacto>(), personaHumana.Apellido, personaHumana.Sexo);
//         return Ok(p);
//     }
//     
//     // POST: api/personas/juridica
//     [HttpPost("juridica")]
//     public IActionResult AddPersonaJuridica(
//         [FromBody] PersonaJuridica personaJuridica
//     )
//     {
//         var p = servicio.CrearJuridica(personaJuridica.Nombre, personaJuridica.RazonSocial, personaJuridica.Direccion, personaJuridica.DocumentoIdentidad,
//             new List<MedioContacto>(), personaJuridica.Tipo, personaJuridica.Rubro);
//         return Ok(p);
//     }
//     
//     // DELETE: api/personas/5
//     [HttpDelete("{id}")]
//     public IActionResult DeletePersona(int id)
//     {
//         servicio.Eliminar(id);
//         return Ok();
//     }
// }