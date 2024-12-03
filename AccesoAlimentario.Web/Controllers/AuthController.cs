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
    public class AuthValidateBody
    {
        public string Token { get; set; } = string.Empty;
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
                _logger.LogError(ex, "Error during registration");
                return Results.StatusCode(500);
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Login([FromBody] AuthValidateBody body)
        {
            var token = body.Token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            try
            {
                // Step 1: Parse the JWT Token
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "aud")?.Value;
                var userEmail = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var userName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                _logger.LogInformation($"UserId: {userId}, UserEmail: {userEmail}, UserName: {userName}");

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userEmail))
                {
                    _logger.LogWarning("Invalid JWT token, missing user details.");
                    return Unauthorized();
                }

                // Step 2: Check if User Exists using GetAsync with IQueryable
                var query = _unitOfWork.UsuarioSistemaRepository.GetQueryable()
                    .Where(u => u.UserName == userName || u.UserName == userEmail);

                var existingUser =
                    await _unitOfWork.UsuarioSistemaRepository
                        .GetAsync(query, false); // No tracking, since we're only checking if it exists

                if (existingUser == null)
                {
                    _logger.LogInformation("User does not exist.");
                    return Ok(new
                    {
                        userExists = false
                    });
                }

                _logger.LogInformation($"User {existingUser.UserName} login.");

                var persona = existingUser.Persona;
                var rolColaborador = persona.Roles.OfType<Colaborador>().FirstOrDefault();
                if (rolColaborador == null)
                {
                    _logger.LogWarning("User does not have a role.");
                }
                var rolTecnico = persona.Roles.OfType<Tecnico>().FirstOrDefault();
                if (rolTecnico == null)
                {
                    _logger.LogWarning("User does not have a role.");
                }

                // Generate jwt token
                var jwtGenerator = new JwtGenerator(60);
                var newToken = jwtGenerator.GenerateToken(existingUser.Id.ToString(),
                [
                    new KeyValuePair<string, string>("colaboradorId", rolColaborador?.Id.ToString() ?? ""),
                    new KeyValuePair<string, string>("tecnicoId", rolTecnico?.Id.ToString() ?? ""),
                    new KeyValuePair<string, string>("name", persona.Nombre),
                    new KeyValuePair<string, string>("profile_picture", existingUser.ProfilePicture),
                    new KeyValuePair<string, string>("contribucionesPreferidas",
                        rolColaborador?.ContribucionesPreferidas.ToString() ?? ""),
                    new KeyValuePair<string, string>("personaTipo",
                        persona switch
                        {
                            PersonaHumana => "colaborador",
                            PersonaJuridica => "tecnico",
                            _ => ""
                        })
                ]);
                // Set cookie
                HttpContext.Response.Cookies.Append("session", newToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(new
                {
                    userExists = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}