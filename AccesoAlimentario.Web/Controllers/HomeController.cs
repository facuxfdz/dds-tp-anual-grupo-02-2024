using System.Diagnostics;
using AccesoAlimentario.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RegistrarPersonaVulnerable()
    {
        return View();
    }

    public IActionResult Reportes()
    {
        return View();
    }

    public IActionResult Premios()
    {
        return View();
    }

    public IActionResult ReporteColaboradores()
    {
        return View();
    }

    public IActionResult Contribuir()
    {
        return View();
    }

    public IActionResult Incidentes()
    {
        return View();
    }

    public IActionResult ImportarColaboradores()
    {
        return View();
    }

    public IActionResult RegistrarColaborador()
    {
        return View();
    }

    public IActionResult ReporteViandas()
    {
        return View();
    }

    public IActionResult ReporteHeladeras()
    {
        return View();
    }

    public IActionResult ReportarFallaTecnica()
    {
        return View();
    }

    public IActionResult Heladeras()
    {
        return View();
    }
    
    public IActionResult Viandas()
    {
        return View();
    }
    
    public IActionResult Contribuciones()
    {
        return View();
    }
    
    public IActionResult Suscripciones()
    {
        return View();
    }
    
    public IActionResult Perfil()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}