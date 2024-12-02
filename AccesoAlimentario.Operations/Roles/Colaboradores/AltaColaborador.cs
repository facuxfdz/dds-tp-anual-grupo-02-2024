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
        
        public AltaColaboradorHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AltaColaboradorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(AltaColaboradorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Alta de colaborador");
            var validator = new AltaColaboradorValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
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
                colaborador.TarjetaColaboracion = tarjeta;
            }
            
            // Guardo en los repositorios
            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.ColaboradorRepository.AddAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}