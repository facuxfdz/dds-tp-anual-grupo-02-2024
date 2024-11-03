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
public class UsuariosController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Post([FromBody] CrearUsuario.CrearUsuarioCommand command)
    {
        return await sender.Send(command);
    }
}