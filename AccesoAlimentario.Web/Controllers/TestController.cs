using AccesoAlimentario.Web.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;


[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Test")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}