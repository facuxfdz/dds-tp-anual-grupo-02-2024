using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Infrastructure.Repositories;
using AccesoAlimentario.API.UseCases.Colaboradores;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradoresController(
    CrearColaboradorHTTP crearColaborador,
    CrearTarjetaColaboracion crearTarjetaColaboracion
    ) : ControllerBase
{
    // POST: api/colaboradores/csv
    // [HttpPost("csv")]
    // public IActionResult ImportarColaboradores([FromForm] IFormFile file)
    // {
    //     // Create stream from file
    //     using var stream = file.OpenReadStream();
    //     // var importador = new ImportadorColaboraciones(new ImportadorCsv(), unitOfWork.ColaboradorRepository);
    //     var importador = new ImportadorColaboraciones(unitOfWork.ColaboradorRepository, new ValidadorImportacionMasiva());
    //     importador.Importar(stream);
    //     var res = unitOfWork.ColaboradorRepository.Get();
    //     return Ok(res);
    // }
    
    [HttpPost]
    // POST: api/colaboradores
    public IActionResult PostColaborador([FromBody] ColaboradorDTO colaborador)
    {
        try
        {
            crearColaborador.CrearColaborador(colaborador);
        }
        catch (PersonaNoExiste e)
        {
            return NotFound(e.Message);
        }
        catch (PersonaYaEsColaborador e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            // Server error
            return StatusCode(500, e.Message);
        }
        return Ok();
    }
    
    // POST api/colaboradores/crearTarjeta
    [HttpPost("crearTarjeta")]
    public IActionResult CrearTarjeta([FromBody] ColaboradorDTO colaborador)
    {
        if(colaborador.Id == null)
        {
            return BadRequest("Id de colaborador no puede ser nulo");
        }

        try
        {
            crearTarjetaColaboracion.CrearTarjeta(colaborador);
        }
        catch (ColaboradorNoExiste e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            // Server error
            return StatusCode(500, e.Message);
        }
        return Ok();
    }
    
}