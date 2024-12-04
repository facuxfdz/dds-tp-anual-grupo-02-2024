using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AccesoAlimentario.Operations.JwtToken;
using AccesoAlimentario.Operations.Roles.Colaboradores;
using AccesoAlimentario.Operations.Roles.Usuarios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Auth;

public static class RegistrarUsuario
{
    public class RegistrarUsuarioCommand : IRequest<IResult>
    {
        // Para conformar el usuario
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public RegisterType RegisterType { get; set; } = RegisterType.Standard;

        // Para conformar el colaborador
        public PersonaRequest Persona { get; set; } = null!;
        public DireccionRequest? Direccion = null!;
        public DocumentoIdentidadRequest Documento = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        public List<TipoContribucion> ContribucionesPreferidas { get; set; } = [];
        public TarjetaColaboracionRequest? Tarjeta { get; set; } = null!;
    }

    internal class RegistrarUsuarioHandle : IRequestHandler<RegistrarUsuarioCommand, IResult>
    {
        private readonly ILogger _logger;
        private readonly ISender _sender;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarUsuarioHandle(ILogger<RegistrarUsuarioHandle> logger,
            ISender sender, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _sender = sender;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registrar usuario");

            var medioContacto = request.MediosDeContacto.Count > 0
                ? request.MediosDeContacto
                : [new EmailRequest { Direccion = request.Email, Preferida = true }];
            
            var altaColaboradorCommand = new AltaColaborador.AltaColaboradorCommand
            {
                Persona = request.Persona,
                Direccion = request.Direccion,
                Documento = request.Documento,
                MediosDeContacto = medioContacto,
                ContribucionesPreferidas = request.ContribucionesPreferidas,
                Tarjeta = request.Tarjeta
            };
            var resultAltaColaborador = await _sender.Send(altaColaboradorCommand, cancellationToken);

            if (resultAltaColaborador is not Microsoft.AspNetCore.Http.HttpResults.Ok<Guid> colaboradorId)
            {
                _logger.LogWarning("Error al crear el colaborador.");
                return Results.BadRequest("Error al crear el colaborador.");
            }

            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(colaboradorId.Value);
            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador no encontrado.");
                return Results.BadRequest("Colaborador no encontrado.");
            }

            var createUserCommand = new CrearUsuario.CrearUsuarioCommand
            {
                PersonaId = colaborador.PersonaId,
                Username = request.Email,
                Password = request.Password,
                ProfilePicture = request.ProfilePicture,
                RegisterType = request.RegisterType
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