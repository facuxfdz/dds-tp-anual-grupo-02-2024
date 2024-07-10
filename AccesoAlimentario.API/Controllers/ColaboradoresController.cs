using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Infraestructura.ImportacionColaboradores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using AppContext = AccesoAlimentario.Core.DAL.AppContext;

namespace AccesoAlimentario.API.Controllers;

[Route ("api/[controller]")]
[ApiController]
public class ColaboradoresController : ControllerBase
{
    private DbContext _context;
    private GenericRepository<Colaborador> _colaboradorRepository;
    public ColaboradoresController()
    {
        var options = new DbContextOptionsBuilder<AppContext>()
            .UseInMemoryDatabase(databaseName: "AccesoAlimentario")
            .Options;
        _context = new AppContext(options);
        _colaboradorRepository = new GenericRepository<Colaborador>((AppContext)_context);
    }
    // POST: api/colaboradores/csv
    [HttpPost]
    public IActionResult ImportarColaboradores([FromForm] IFormFile file)
    {
        // Create stream from file
        using var stream = file.OpenReadStream();
        var importador = new ImportadorColaboraciones(new ImportadorCsv(), _colaboradorRepository);
        importador.Importar(stream);
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