using Microsoft.AspNetCore.Mvc;
using AccesoAlimentario.Operations.Auth;
using AccesoAlimentario.Operations.Roles;
using MediatR;

namespace AccesoAlimentario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ISender _sender; // for CrearUsuario

        public AuthController(ILogger<AuthController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost("registrar")]
        public async Task<IResult> Registrar([FromBody] RegistrarUsuario.RegistrarUsuarioCommand command)
        {
            try
            {
                return await _sender.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el registro");
                return Results.StatusCode(500);
            }
        }

        [HttpPost("validar")]
        public async Task<IResult> Validar([FromBody] ValidarUsuario.ValidarUsuarioCommand command)
        {
            try
            {
                return await _sender.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la validación");
                return Results.StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LogInUsuario.LogInUsuarioCommand command)
        {
            try
            {
                return await _sender.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login");
                return Results.StatusCode(500);
            }
        }

        [HttpPost("logout")]
        public async Task<IResult> Logout()
        {
            try
            {
                return await _sender.Send(new LogOutUsuario.LogOutUsuarioCommand());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el logout");
                return Results.StatusCode(500);
            }
        }

        [HttpPost("password/validar")]
        public async Task<IResult> PasswordValidar([FromBody] ValidarPassword.ValidarPasswordCommand command)
        {
            try
            {
                return await _sender.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la validación de la contraseña");
                return Results.StatusCode(500);
            }
        }

        [HttpGet("perfil")]
        public async Task<IResult> Perfil()
        {
            try
            {
                return await _sender.Send(new ObtenerPerfil.ObtenerPerfilCommand());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la obtención del perfil");
                return Results.StatusCode(500);
            }
        }
        
        [HttpPut("perfil")]
        public async Task<IResult> Perfil([FromBody] ActualizarPerfil.ActualizarPerfilCommand command)
        {
            try
            {
                return await _sender.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la actualización del perfil");
                return Results.StatusCode(500);
            }
        }
    }
}