using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Usuarios;

public static class CrearUsuario
{
    public class CrearUsuarioCommand : IRequest<IResult>
    {
        public Guid PersonaId { get; set; } = Guid.Empty;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    
    public class Handler : IRequestHandler<CrearUsuarioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(request.PersonaId);
            if (persona == null)
            {
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
            
            var builder = new NotificacionUsuarioCreadoBuilder(usuario.UserName, usuario.Password);
            persona.EnviarNotificacion(builder.CrearNotificacion());

            return Results.Ok();
        }
    }
}