using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Infraestructura.ImportacionColaboradores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using AppDbContext = AccesoAlimentario.Core.DAL.AppDbContext;

namespace AccesoAlimentario.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradoresController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    public ColaboradoresController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // POST: api/colaboradores/csv
    [HttpPost("csv")]
    public IActionResult ImportarColaboradores([FromForm] IFormFile file)
    {
        // Create stream from file
        using var stream = file.OpenReadStream();
        var importador = new ImportadorColaboraciones(new ImportadorCsv(), _unitOfWork.ColaboradorRepository);
        importador.Importar(stream);
        return Ok();
    }

    // GET: api/colaboradores
    [HttpGet]
    public IActionResult GetColaboradores()
    {
        var colaboradores = _unitOfWork.ColaboradorRepository.Get();
        return Ok(colaboradores);
    }

    [HttpPost("persona-humana")]
    public IActionResult AddPersonaHumana(
        [FromBody] PersonaHumana personaHumana
    )
    {
        _unitOfWork.PersonaHumanaRepository.Insert(personaHumana);
        return Ok();
    }
    
    [HttpGet("persona-humana")]
    public IActionResult GetPersonasHumana()
    {
        var personaHumana = _unitOfWork.PersonaHumanaRepository.Get();
        return Ok(personaHumana);
    }
}