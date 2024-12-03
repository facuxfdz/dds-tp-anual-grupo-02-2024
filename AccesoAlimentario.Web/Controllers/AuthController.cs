using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Auth;
using AccesoAlimentario.Operations.JwtToken;
using MediatR;

namespace AccesoAlimentario.Api.Controllers
{
    public class AuthLoginBody
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ISender _sender; // for CrearUsuario
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(ILogger<AuthController> logger, ISender sender, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _sender = sender;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegistrarUsuario.RegistrarUsuarioCommand command)
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

        [HttpPost("validate")]
        public async Task<IResult> Validate([FromBody] ValidarUsuario.ValidarUsuarioCommand command)
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
    }
}