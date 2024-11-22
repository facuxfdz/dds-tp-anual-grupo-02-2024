using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Usuarios;

public class CrearUsuarioSSO
{
    public class CrearUsuarioSSOCommand : IRequest<IResult>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class CrearUsuarioSSOHandler : IRequestHandler<CrearUsuarioSSOCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CrearUsuarioSSOHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CrearUsuarioSSOHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(CrearUsuarioSSOCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creando persona para usuario SSO");
            var persona = new PersonaHumana
            {
                Nombre = request.Username,
                Apellido = request.Username
            };
            
            await _unitOfWork.PersonaRepository.AddAsync(persona);

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