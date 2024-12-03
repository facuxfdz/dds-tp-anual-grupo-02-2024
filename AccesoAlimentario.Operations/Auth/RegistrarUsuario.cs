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
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarUsuarioHandle(ILogger<RegistrarUsuarioHandle> logger,
            ISender sender, IHttpContextAccessor httpContext, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _sender = sender;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registrando usuario");

            var altaColaboradorCommand = new AltaColaborador.AltaColaboradorCommand
            {
                Persona = request.Persona,
                Direccion = request.Direccion,
                Documento = request.Documento,
                MediosDeContacto = request.MediosDeContacto,
                ContribucionesPreferidas = request.ContribucionesPreferidas,
                Tarjeta = request.Tarjeta
            };
            var resultAltaColaborador = await _sender.Send(altaColaboradorCommand, cancellationToken);

            if (resultAltaColaborador is not Microsoft.AspNetCore.Http.HttpResults.Ok<Guid> colaboradorId)
            {
                _logger.LogWarning("Colaborador creation failed.");
                return Results.BadRequest("Colaborador creation failed.");
            }

            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(colaboradorId.Value);
            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador not found.");
                return Results.BadRequest("Colaborador not found.");
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
            if (result is Microsoft.AspNetCore.Http.HttpResults.Ok<Guid> usuarioId)
            {
                _logger.LogInformation("User created successfully.");
            }
            else
            {
                _logger.LogWarning("User creation failed.");
                return Results.BadRequest("User creation failed.");
            }

            var persona = colaborador.Persona;
            // Generate jwt token
            var jwtGenerator = new JwtGenerator(60);
            var token = jwtGenerator.GenerateToken(usuarioId.Value.ToString(), 
            [
                new KeyValuePair<string, string>("colaboradorId", colaborador.Id.ToString()),
                new KeyValuePair<string, string>("tecnicoId", ""),
                new KeyValuePair<string, string>("name", persona.Nombre),
                new KeyValuePair<string, string>("profile_picture", request.ProfilePicture),
                new KeyValuePair<string, string>("contribucionesPreferidas",
                    colaborador.ContribucionesPreferidas.ToString() ?? ""),
                new KeyValuePair<string, string>("personaTipo",
                    persona switch
                    {
                        PersonaHumana => "colaborador",
                        PersonaJuridica => "tecnico",
                        _ => ""
                    }),
            ]);
            // Set cookie

            _httpContext.HttpContext!.Response.Cookies.Append("session", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Results.Ok(new
            {
                userCreated = true
            });
        }
    }
}