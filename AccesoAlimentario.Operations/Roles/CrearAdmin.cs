using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Passwords;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Operations.Roles.Usuarios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles;

public static class CrearAdmin
{
    public class CrearAdminCommand : IRequest<IResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        public PersonaRequest Persona { get; set; } = null!;
        public DireccionRequest? Direccion = null!;
        public DocumentoIdentidadRequest Documento = null!;
    }
    
    internal class CrearAdminHandler : IRequestHandler<CrearAdminCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISender _sender;

        public CrearAdminHandler(ILogger<CrearAdminHandler> logger,
            ISender sender, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(CrearAdminCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Crear admin");

            var persona = _mapper.Map<Persona>(request.Persona);
            if (request.Direccion != null)
            {
                var direccion = _mapper.Map<Direccion>(request.Direccion);
                persona.Direccion = direccion;
            }
            persona.DocumentoIdentidad = _mapper.Map<DocumentoIdentidad>(request.Documento);
            persona.MediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);
            
            if (request.MediosDeContacto.Count == 0)
            {
                persona.MediosDeContacto.Add(new Email { Direccion = request.Email, Preferida = true });
            }
            
            var colaborador = new Colaborador
            {
                Persona = persona,
                ContribucionesPreferidas = [],
            };
            
            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.ColaboradorRepository.AddAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();

            var email = persona.MediosDeContacto.OfType<Email>().First().Direccion;
            
            var createUserCommand = new CrearUsuario.CrearUsuarioCommand
            {
                PersonaId = colaborador.PersonaId,
                Username = email,
                Password = request.Password != "" ? request.Password : PasswordManager.CrearPassword(),
                ProfilePicture = "",
                RegisterType = RegisterType.Standard,
                IsAdmin = true,
            };

            var result = await _sender.Send(createUserCommand, cancellationToken);
            if (result is Microsoft.AspNetCore.Http.HttpResults.Ok<Guid>)
            {
                _logger.LogInformation("Usuario creado con éxito.");
            }
            else
            {
                _logger.LogWarning("Error al crear el usuario.");
                return Results.BadRequest("Error al crear el usuario.");
            }
            
            return Results.Ok();
        }
    }
}