using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Passwords;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AccesoAlimentario.Operations.Roles.Usuarios;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class AltaColaborador
{
    public class AltaColaboradorCommand : IRequest<IResult>
    {
        // Para conformar la persona
        public PersonaRequest Persona { get; set; } = null!;
        public DireccionRequest? Direccion = null!;
        public DocumentoIdentidadRequest Documento = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        // Para conformar el colaborador
        public List<TipoContribucion> ContribucionesPreferidas { get; set; } = [];
        public TarjetaColaboracionRequest? Tarjeta { get; set; } = null!;
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public RegisterType? RegisterType { get; set; }
    }
    
    // Validaciones
    public class AltaColaboradorValidator : AbstractValidator<AltaColaboradorCommand>
    {
        public AltaColaboradorValidator()
        {
            RuleFor(x => x.Persona)
                .NotNull();
            // La direccion es opcional
            // RuleFor(x => x.Direccion)
            //     .NotNull();
            // La fecha de nacimiento es opcional
            RuleFor(x => x.Documento)
                .NotNull();
            // Debe poseer al menos un medio de contacto
            RuleFor(x => x.MediosDeContacto)
                .NotNull();
            RuleFor(x => x.ContribucionesPreferidas)
                .NotNull();
            // La tarjeta es solo requerida cuando entre las formas de contribuccion preferidas
            // se encuentra la distribucion o donacion de viandas
            // RuleFor(x => x.Tarjeta)
            //     .NotNull();
        }
    }
    
    // Handler
    public class AltaColaboradorHandler : IRequestHandler<AltaColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISender _sender;
        
        public AltaColaboradorHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AltaColaboradorHandler> logger, ISender sender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _sender = sender;
        }
        
        public async Task<IResult> Handle(AltaColaboradorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Alta de colaborador");
            var validator = new AltaColaboradorValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Datos invalidos.");
                return Results.Problem();
            }
            
            // Conforma la persona
            var persona = _mapper.Map<Persona>(request.Persona);
            if (request.Direccion != null)
            {
                var direccion = _mapper.Map<Direccion>(request.Direccion);
                persona.Direccion = direccion;
            }
            persona.DocumentoIdentidad = _mapper.Map<DocumentoIdentidad>(request.Documento);
            persona.MediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);
            
            // Creo al colaborador
            var colaborador = new Colaborador
            {
                Persona = persona,
                ContribucionesPreferidas = request.ContribucionesPreferidas,
            };
            
            // Si la tarjeta no es nula, la agrego al colaborador
            if (request.Tarjeta != null)
            {
                var tarjeta = _mapper.Map<TarjetaColaboracion>(request.Tarjeta);
                tarjeta.Propietario = colaborador;
                colaborador.TarjetaColaboracion = tarjeta;
            }
            
            // Guardo en los repositorios
            
            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.ColaboradorRepository.AddAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();

            var email = persona.MediosDeContacto.OfType<Email>().First().Direccion;
            
            var createUserCommand = new CrearUsuario.CrearUsuarioCommand
            {
                PersonaId = colaborador.Persona.Id,
                Username = email,
                Password = (request.Password != "" ? request.Password : PasswordManager.CrearPassword()) ?? PasswordManager.CrearPassword(),
                ProfilePicture = request.ProfilePicture ?? "",
                RegisterType = request.RegisterType ?? RegisterType.Standard,
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
            
            return Results.Ok(colaborador.Id);
        }
    }
}