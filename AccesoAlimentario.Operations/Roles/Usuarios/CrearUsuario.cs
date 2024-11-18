using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Usuarios;

public static class CrearUsuario
{
    public class CrearUsuarioCommand : IRequest<IResult>
    {
        public Guid PersonaId { get; set; } = Guid.Empty;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class CrearUsuarioHandler : IRequestHandler<CrearUsuarioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CrearUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CrearUsuarioHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creando usuario");
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(request.PersonaId);
            if (persona == null)
            {
                _logger.LogWarning("Persona no encontrada");
                return Results.NotFound();
            }

            var usuario = new UsuarioSistema
            {
                PersonaId = persona.Id,
                UserName = request.Username,
                Password = request.Password
            };

            await _unitOfWork.UsuarioSistemaRepository.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Usuario creado");

            var builder = new NotificacionUsuarioCreadoBuilder(usuario.UserName, usuario.Password);
            persona.EnviarNotificacion(builder.CrearNotificacion());
            _logger.LogInformation("Notificacion enviada");

            return Results.Ok();
        }
    }
}