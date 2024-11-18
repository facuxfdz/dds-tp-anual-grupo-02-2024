using System.Diagnostics;
using System.Security.Claims;
using AccesoAlimentario.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public LoginController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult GoogleSignIn(string returnUrl)
    {
        var redirectUrl = Url.Action("SigninGoogle", "Login", null, Request.Scheme);
        Console.WriteLine($"redirectUrl: {redirectUrl}");
        return new ChallengeResult(
            GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties { RedirectUri = redirectUrl }
        );
    }
    
    [HttpGet("callback-google")]
    public async Task<IActionResult> SigninGoogle()
    {
        Console.WriteLine("GoogleCallback");
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            return RedirectToAction("Index");
        }

        // You can extract user information here
        var userClaims = authenticateResult.Principal?.Identities.FirstOrDefault()?.Claims;
        var email = userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

        // Perform additional logic here, such as creating a session or a local user record.

        return RedirectToAction("Index", "Home");
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