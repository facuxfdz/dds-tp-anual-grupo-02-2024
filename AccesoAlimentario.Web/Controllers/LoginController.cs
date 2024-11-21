using AccesoAlimentario.Operations.Roles.Usuarios; // for CrearUsuario
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AccesoAlimentario.Core.DAL;
using MediatR;
using AccesoAlimentario.Core.Entities.Roles;
using Castle.Components.DictionaryAdapter.Xml;

namespace AccesoAlimentario.Api.Controllers
{
    public class AuthLoginBody
    {
        public string token { get; set; }
    }
    
    [ApiController]
    [Route("api/auth/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMediator _mediator; // for CrearUsuario
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(ILogger<LoginController> logger, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthLoginBody body)
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
                    // Step 3: If User doesn't exist, create new user
                    var createUserCommand = new CrearUsuario.CrearUsuarioCommand
                    {
                        PersonaId = Guid.NewGuid(), // You should obtain the correct PersonaId
                        Username = userName ?? userEmail, // Use username or email
                        Password = "" // For users logging in via JWT, we set an empty password
                    };

                    var result = await _mediator.Send(createUserCommand);
                    if (result.GetType() == typeof(OkResult))
                    {
                        _logger.LogInformation("User created successfully.");
                    }
                    else
                    {
                        _logger.LogWarning("User creation failed.");
                        return BadRequest("User creation failed.");
                    }
                }

                // Step 4: Return some initial user data, such as the user's email and username
                var userResponse = new
                {
                    userId = userId,
                    userEmail = userEmail,
                    userName = userName,
                    token = token // Optionally return the token for client-side usage
                };

                // Set the session cookie (you can modify this based on your needs)
                Response.Cookies.Append("session", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true, // Set this to false if you're testing locally without HTTPS
                    Expires = DateTime.UtcNow.AddHours(1) // Set session duration
                });

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
