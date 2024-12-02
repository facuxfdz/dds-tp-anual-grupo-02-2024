using AccesoAlimentario.Operations.Roles.Usuarios; // for CrearUsuario
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Web.Utils;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AccesoAlimentario.Api.Controllers
{
    public class AuthValidateBody
    {
        public string token { get; set; }
    }
    
    public class AuthRegisterBody
    {
        public string email { get; set; }
        public string password { get; set; }
        public string profile_picture { get; set; }
        public string register_type { get; set; }
        public string user_type { get; set; }
        public DireccionRequest direccion { get; set; }
        public DocumentoIdentidadRequest documento { get; set; }
        public PersonaRequest persona { get; set; }
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
        public async Task<IActionResult> Register([FromBody] AuthRegisterBody body)
        {
            var email = body.email;
            var password = body.password;
            var profile_picture = body.profile_picture;
            var register_type = body.register_type;
            var user_type = body.user_type;
            var persona = body.persona;
            var direccion = body.direccion;
            var documento = body.documento;

            if (string.IsNullOrEmpty(email) || (string.IsNullOrEmpty(password) && register_type == "standard") || string.IsNullOrEmpty(register_type))
            {
                return BadRequest("Email, password and register_type are required");
            }

            try
            {

                var altaColaboradorCommand = new AltaColaborador.AltaColaboradorCommand
                {
                    Persona = persona,
                    Direccion = direccion,
                    Documento = documento
                };
                var res = await _sender.Send(altaColaboradorCommand);
                Guid personaId = Guid.Empty;
                if (res is Microsoft.AspNetCore.Http.HttpResults.Ok<Colaborador> ok)
                {
                    _logger.LogInformation("Colaborador created successfully.");
                    if (ok.Value != null) personaId = ok.Value.PersonaId;
                }
                else
                {
                    _logger.LogWarning("Colaborador creation failed.");
                    return BadRequest("Colaborador creation failed.");
                }
                
                Console.WriteLine(personaId);
                var createUserCommand = new CrearUsuario.CrearUsuarioCommand
                {
                    PersonaId = personaId,
                    Username = email,
                    Password = password,
                    ProfilePicture = profile_picture,
                    RegisterType = register_type
                };

                var result = await _sender.Send(createUserCommand);
                if (result is Microsoft.AspNetCore.Http.HttpResults.Ok)
                {
                    _logger.LogInformation("User created successfully.");
                }
                else
                {
                    _logger.LogWarning("User creation failed.");
                    return BadRequest("User creation failed.");
                }
                // Generate jwt token
                var jwtGenerator = new JWTGenerator(60);
                var token = jwtGenerator.GenerateToken(personaId.ToString(), email);
                // Set cookie
                HttpContext.Response.Cookies.Append("session", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return Ok(new
                {
                    userCreated = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Login([FromBody] AuthValidateBody body)
        {
            var token = body.token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            try
            {
                // Step 1: Parse the JWT Token
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "aud" )?.Value;
                var userEmail = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var userName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                _logger.LogInformation($"UserId: {userId}, UserEmail: {userEmail}, UserName: {userName}");

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userEmail))
                {
                    _logger.LogWarning("Invalid JWT token, missing user details.");
                    return Unauthorized();
                }

                // Step 2: Check if User Exists using GetAsync with IQueryable
                var query = _unitOfWork.UsuarioSistemaRepository.GetQueryable().Where(u => u.UserName == userName || u.UserName == userEmail);

                var existingUser = await _unitOfWork.UsuarioSistemaRepository.GetAsync(query, false); // No tracking, since we're only checking if it exists

                if (existingUser == null)
                {
                    _logger.LogInformation("User does not exist.");
                    return Ok(new
                    {
                        userExists = false
                    });
                }

                _logger.LogInformation($"User {existingUser.UserName} login.");
                
                // Generate jwt token
                var jwtGenerator = new JWTGenerator(60);
                var newToken = jwtGenerator.GenerateToken(existingUser.Id.ToString(), existingUser.UserName);
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
