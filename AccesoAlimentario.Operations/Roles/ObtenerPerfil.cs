using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Tokens;
using AccesoAlimentario.Operations.Dto.Responses.Personas;
using AccesoAlimentario.Operations.Dto.Responses.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles;

public static class ObtenerPerfil
{
    public class ObtenerPerfilCommand : IRequest<IResult>
    {
    }

    internal class ObtenerPerfilHandler : IRequestHandler<ObtenerPerfilCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ObtenerPerfilHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            ILogger<ObtenerPerfilHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerPerfilCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener perfil de usuario");
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["session"];
            if (cookie == null)
            {
                _logger.LogWarning("No se encontró la cookie de sesión");
                return Results.Unauthorized();
            }

            var usuarioId = TokenUsuario.ObtenerId(cookie);

            if (usuarioId == Guid.Empty)
            {
                _logger.LogWarning("No se encontró el id del usuario en la cookie de sesión");
                return Results.Unauthorized();
            }

            var rol = await _unitOfWork.RolRepository.GetByIdAsync(usuarioId);
            if (rol == null)
            {
                _logger.LogWarning("No se encontró el rol del usuario");
                return Results.BadRequest();
            }

            var persona = rol.Persona;
            if (persona == null)
            {
                _logger.LogWarning("No se encontró la persona del usuario");
                return Results.BadRequest();
            }

            var responsePersona = _mapper.Map<PersonaResponse>(persona);

            var rolResponse = persona.Roles
                .Where(x=> x is not UsuarioSistema)
                .Select(
                r => _mapper.Map(r, r.GetType(), typeof(RolResponseMinimo))
            ).ToList();

            return Results.Ok(
                new
                {
                    Persona = responsePersona,
                    Roles = rolResponse
                }
            );
        }
    }
}