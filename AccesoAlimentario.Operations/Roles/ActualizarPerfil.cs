using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Tokens;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles;

public static class ActualizarPerfil
{
    public class ActualizarPerfilCommand : IRequest<IResult>
    {
        public PersonaRequest Persona { get; set; } = null!;
        public DireccionRequest? Direccion = null!;
        public DocumentoIdentidadRequest Documento = null!;

        public List<TipoContribucion>? ContribucionesPreferidas { get; set; } = [];
        public TarjetaColaboracionRequest? Tarjeta { get; set; } = null!;
        public float? AreaCoberturaLatitud { get; set; } = 0;
        public float? AreaCoberturaLongitud { get; set; } = 0;
        public float? AreaCoberturaRadio { get; set; } = 0;
    }

    internal class ActualizarPerfilHandler : IRequestHandler<ActualizarPerfilCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ActualizarPerfilHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            ILogger<ActualizarPerfilHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ActualizarPerfilCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Actualizar perfil de usuario");

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

            var personaRequest = _mapper.Map<Persona>(request.Persona);
            if (request.Direccion != null)
            {
                var direccion = _mapper.Map<Direccion>(request.Direccion);
                personaRequest.Direccion = direccion;
            }

            personaRequest.DocumentoIdentidad = _mapper.Map<DocumentoIdentidad>(request.Documento);

            var colaborador = persona.Roles.OfType<Colaborador>().FirstOrDefault();

            if (request.Tarjeta != null && colaborador != null)
            {
                var tarjeta = _mapper.Map<TarjetaColaboracion>(request.Tarjeta);
                if (colaborador.TarjetaColaboracion != null)
                {
                    colaborador.TarjetaColaboracion.Codigo = tarjeta.Codigo;
                }
                else
                {
                    tarjeta.Propietario = colaborador;
                    colaborador.TarjetaColaboracion = tarjeta;
                    await _unitOfWork.TarjetaColaboracionRepository.AddAsync(tarjeta);
                }
            }
            
            if (request.ContribucionesPreferidas != null && colaborador != null)
            {
                colaborador.ContribucionesPreferidas = request.ContribucionesPreferidas;
            }

            var tecnico = persona.Roles.OfType<Tecnico>().FirstOrDefault();
            if (tecnico != null && request.AreaCoberturaLatitud != null && request.AreaCoberturaLongitud != null &&
                request.AreaCoberturaRadio != null)
            {
                tecnico.AreaCobertura
                    .ActualizarArea(request.AreaCoberturaLatitud.Value, request.AreaCoberturaLongitud.Value,
                        request.AreaCoberturaRadio.Value);
            }
            
            persona.Nombre = personaRequest.Nombre;
            
            if (persona is PersonaHumana personaHumana)
            {
                personaHumana.Apellido = (personaRequest as PersonaHumana)!.Apellido;
                personaHumana.Sexo = (request.Persona as PersonaHumanaRequest)!.Sexo;
            } else if (persona is PersonaJuridica personaJuridica)
            {
                personaJuridica.RazonSocial = (personaRequest as PersonaJuridica)!.RazonSocial;
                personaJuridica.Tipo = (request.Persona as PersonaJuridicaRequest)!.Tipo;
                personaJuridica.Rubro = (personaRequest as PersonaJuridica)!.Rubro;
            }
            
            if (personaRequest.Direccion != null && persona.Direccion != null)
            {
                persona.Direccion.Actualizar(personaRequest.Direccion.Calle, personaRequest.Direccion.Numero,
                    personaRequest.Direccion.Localidad, personaRequest.Direccion.CodigoPostal,
                    personaRequest.Direccion.Piso, personaRequest.Direccion.Departamento);
            }
            else
            {
                if (personaRequest.Direccion != null)
                {
                    var direccion = _mapper.Map<Direccion>(personaRequest.Direccion);
                    persona.Direccion = direccion;
                    await _unitOfWork.DireccionRepository.AddAsync(direccion);
                }
            }
            
            if (personaRequest.DocumentoIdentidad != null && persona.DocumentoIdentidad != null)
            {
                persona.DocumentoIdentidad.Actualizar(personaRequest.DocumentoIdentidad.TipoDocumento,
                    personaRequest.DocumentoIdentidad.NroDocumento, personaRequest.DocumentoIdentidad.FechaNacimiento);
            }
            else
            {
                if (personaRequest.DocumentoIdentidad != null)
                {
                    var documentoIdentidad = _mapper.Map<DocumentoIdentidad>(personaRequest.DocumentoIdentidad);
                    persona.DocumentoIdentidad = documentoIdentidad;
                    await _unitOfWork.DocumentoIdentidadRepository.AddAsync(documentoIdentidad);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}