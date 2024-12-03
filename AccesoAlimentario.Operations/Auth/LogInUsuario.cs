using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
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

            var persona = existingUser.Persona;
            var rolColaborador = persona.Roles.OfType<Colaborador>().FirstOrDefault();
            var rolTecnico = persona.Roles.OfType<Tecnico>().FirstOrDefault();

            // Generate jwt token
            var jwtGenerator = new JwtGenerator(60);
            var contribucionesPreferidasInt =
                rolColaborador?.ContribucionesPreferidas.Select(c => (int)c).ToArray() ?? [];
            var newToken = jwtGenerator.GenerateToken(existingUser.Id.ToString(),
            [
                new KeyValuePair<string, string>("colaboradorId", rolColaborador?.Id.ToString() ?? ""),
                new KeyValuePair<string, string>("tecnicoId", rolTecnico?.Id.ToString() ?? ""),
                new KeyValuePair<string, string>("name", persona.Nombre),
                new KeyValuePair<string, string>("profile_picture", existingUser.ProfilePicture),
                new KeyValuePair<string, string>("contribucionesPreferidas",
                    string.Join(",", contribucionesPreferidasInt)),
                new KeyValuePair<string, string>("personaTipo",
                    persona switch
                    {
                        PersonaHumana => "humana",
                        PersonaJuridica => "juridica",
                        _ => ""
                    })
            ]);
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