using AccesoAlimentario.Operations.Roles.Usuarios;
using AccesoAlimentario.Web.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccesoAlimentario.Web.Controllers;

[ApiExplorerSettings(GroupName = ApiConstants.AccesoAlimentarioName)]
[Produces("application/json")]
[Route("api/[controller]")]
[Tags("Usuarios")]
[ApiController]
public class UsuariosController(ISender sender, ILogger<UsuariosController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Post([FromBody] CrearUsuario.CrearUsuarioCommand command)
    {
        try
        {
            return await sender.Send(command);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error al crear un usuario");
            return Results.StatusCode(500);
        }
    }
}