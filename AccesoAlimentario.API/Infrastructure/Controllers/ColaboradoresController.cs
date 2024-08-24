using AccesoAlimentario.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradoresController(UnitOfWork unitOfWork) : ControllerBase
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

    // GET: api/colaboradores
    [HttpGet]
    public IActionResult GetColaboradores()
    {
        var colaboradores = unitOfWork.ColaboradorRepository.Get();
        return Ok(colaboradores);
    }
    
}