using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Infraestructura.ImportacionColaboradores;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.API.Controllers;

[Route ("api/[controller]")]
[ApiController]
public class ColaboradoresController : ControllerBase
{
    private GenericRepository<Colaborador> _colaboradorRepository;
    public ColaboradoresController(AppDbContext context)
    {
        _colaboradorRepository = new GenericRepository<Colaborador>(context);
    }
    // POST: api/colaboradores/csv
    [HttpPost]
    [Route("csv")]
    public IActionResult ImportarColaboradores(IFormFile file)
    {
        // Create stream from file
        using var stream = file.OpenReadStream();
        var importador = new ImportadorColaboraciones(new ImportadorCsv(), _colaboradorRepository);
        importador.Importar(stream);
        var res = _colaboradorRepository.Get();
        var t = 1;
        return Ok();
    }
    
    // GET: api/colaboradores
    [HttpGet]
    public IActionResult GetColaboradores()
    {
        var colaboradores = _colaboradorRepository.Get();
        return Ok(colaboradores);
    }
}