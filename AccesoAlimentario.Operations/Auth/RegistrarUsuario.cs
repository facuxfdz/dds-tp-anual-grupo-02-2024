using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AccesoAlimentario.Operations.Roles.Colaboradores;
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

        public RegistrarUsuarioHandle(ILogger<RegistrarUsuarioHandle> logger,
            ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public async Task<IResult> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Registrar usuario - {request.Email}");

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
                Tarjeta = request.Tarjeta,
                Password = request.Password,
                ProfilePicture = request.ProfilePicture,
                RegisterType = request.RegisterType,
            };
            var resultAltaColaborador = await _sender.Send(altaColaboradorCommand, cancellationToken);

            return
                resultAltaColaborador is not Microsoft.AspNetCore.Http.HttpResults.Ok<Guid>
                    ? resultAltaColaborador
                    : Results.Ok();
        }
    }
}