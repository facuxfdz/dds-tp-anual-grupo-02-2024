using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Passwords;
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
        public string ProfilePicture { get; set; } = null!;
        public RegisterType RegisterType { get; set; } = RegisterType.Standard;
        public bool IsAdmin { get; set; } = false;
    }

    public class CrearUsuarioHandler : IRequestHandler<CrearUsuarioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CrearUsuarioHandler(IUnitOfWork unitOfWork, ILogger<CrearUsuarioHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Crear Usuario - {PersonaId}", request.PersonaId);
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(request.PersonaId);
            if (persona == null)
            {
                _logger.LogWarning("Persona no encontrada - {PersonaId}", request.PersonaId);
                return Results.NotFound();
            }

            var passwordHashed = PasswordManager.HashPassword(request.Password);

            var usuario = new UsuarioSistema
            {
                Persona = persona,
                UserName = request.Username,
                Password = passwordHashed,
                ProfilePicture = request.ProfilePicture,
                RegisterType = request.RegisterType,
                IsAdmin = request.IsAdmin
            };

            await _unitOfWork.UsuarioSistemaRepository.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            var builder = new NotificacionUsuarioCreadoBuilder(usuario.UserName, request.Password);
            persona.EnviarNotificacion(builder.CrearNotificacion());
            _logger.LogInformation("Notificacion enviada");

            return Results.Ok(usuario.Id);
        }
    }
}