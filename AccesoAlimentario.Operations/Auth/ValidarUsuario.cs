using System.IdentityModel.Tokens.Jwt;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Tokens;
using AccesoAlimentario.Operations.JwtToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Auth;

public static class ValidarUsuario
{
    public class ValidarUsuarioCommand : IRequest<IResult>
    {
        public string Token { get; set; } = string.Empty;
    }

    internal class ValidarUsuarioHandler : IRequestHandler<ValidarUsuarioCommand, IResult>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;

        public ValidarUsuarioHandler(ILogger<ValidarUsuarioHandler> logger, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<IResult> Handle(ValidarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validar usuario");

            var token = request.Token;
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token es requerido");
                return Results.BadRequest("Token es requerido");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "aud")?.Value;
            var userEmail = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var userName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            _logger.LogInformation($"UserId: {userId}, UserEmail: {userEmail}, UserName: {userName}");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userEmail))
            {
                _logger.LogWarning("Token inválido");
                return Results.Unauthorized();
            }

            var query = _unitOfWork.UsuarioSistemaRepository.GetQueryable()
                .Where(u => u.UserName == userName || u.UserName == userEmail);

            var existingUser =
                await _unitOfWork.UsuarioSistemaRepository
                    .GetAsync(query);

            if (existingUser == null)
            {
                _logger.LogInformation("Usuario no existe");
                return Results.Ok(new
                {
                    userExists = false,
                    token = ""
                });
            }

            _logger.LogInformation($"User {existingUser.UserName} login.");
            var newToken = TokenUsuario.GenerarToken(existingUser);
            
            // Set cookie
            _httpContext.HttpContext!.Response.Cookies.Append("session", newToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Results.Ok(new
            {
                userExists = true,
                token = newToken
            });
        }
    }
}