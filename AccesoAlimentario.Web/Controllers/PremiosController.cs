using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Premios;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PremiosController : ControllerBase
{
  private readonly ILogger<PremiosController> _logger;
  private readonly IUnitOfWork _unitOfWork;
  public PremiosController(ILogger<PremiosController> logger, IUnitOfWork unitOfWork)
  {
    _logger = logger;
    _unitOfWork = unitOfWork;
  }
  
  [HttpGet]
  public async Task<IActionResult> GetPremios([FromQuery] int? categoria = null, [FromQuery] string? nombre = null, [FromQuery] int? puntosNecesarios = null)
  {
    try
    {
      var query = _unitOfWork.PremioRepository.GetQueryable();
      if (categoria != 0)
      {
        query = query.Where(p => p.Rubro == TipoRubro.Electronica); // TODO: Harcodeado
      }
      if (!string.IsNullOrEmpty(nombre))
      {
        query = query.Where(p => p.Nombre.Contains(nombre));
      }
      if (puntosNecesarios != null && puntosNecesarios != 0)
      {
        query = query.Where(p => p.PuntosNecesarios == puntosNecesarios);
      }
      var premios = await _unitOfWork.PremioRepository.GetAsync(query);
      return Ok(premios);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error al obtener premios");
      return StatusCode(500);
    }
  }
}