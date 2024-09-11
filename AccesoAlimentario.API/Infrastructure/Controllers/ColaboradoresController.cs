using AccesoAlimentario.API.UseCases.Colaboradores;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.Personas.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Colaboradores;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Infrastructure.Controllers;

public class UploadFileRequest
{
    public IFormFile? File { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class ColaboradoresController(
    CrearColaboradorHTTP crearColaborador,
    CrearTarjetaColaboracion crearTarjetaColaboracion,
    ImportarColaboraciones importarColaboraciones
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
    
    // POST: api/colaboradores/importarColaboraciones
    [HttpPost("importarColaboraciones")]
    public IActionResult ImportarColaboraciones([FromForm] UploadFileRequest file)
    {
        // Create stream from file
        var fileForm = file.File;
        if (fileForm == null)
        {
            return BadRequest("No se ha enviado un archivo");
        }
        var stream = fileForm.OpenReadStream();
        
        try
        {
            importarColaboraciones.ImportarCsv(stream);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok();
    }
    
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