using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Tokens;
using AccesoAlimentario.Operations.JwtToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Auth;

public static class LogInUsuario
{
    public class LogInUsuarioCommand : IRequest<IResult>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal class LogInUsuarioHandler : IRequestHandler<LogInUsuarioCommand, IResult>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;

        public LogInUsuarioHandler(ILogger<LogInUsuarioHandler> logger, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<IResult> Handle(LogInUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("LogIn usuario");

            var query = _unitOfWork.UsuarioSistemaRepository.GetQueryable()
                .Where(u => u.UserName == request.Username && u.Password == request.Password);
            var existingUser = await _unitOfWork.UsuarioSistemaRepository.GetAsync(query, false);
            if (existingUser == null)
            {
                return Results.Unauthorized();
            }
            
            // Generate jwt token
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