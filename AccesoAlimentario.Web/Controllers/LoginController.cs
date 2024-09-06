using System.Diagnostics;
using AccesoAlimentario.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public LoginController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}