using AccesoAlimentario.Web.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;


[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Tags("Test")]
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}