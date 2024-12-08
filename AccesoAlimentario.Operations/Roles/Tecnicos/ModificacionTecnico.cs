using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class ModificacionTecnico
{
    public class ModificacionTecnicoCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public PersonaRequest Persona { get; set; } = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        public float AreaCoberturaLatitud { get; set; } = 0;
        public float AreaCoberturaLongitud { get; set; } = 0;
        public float AreaCoberturaRadio { get; set; } = 0;
    }
    
    public class ModificacionTecnicoValidator : AbstractValidator<ModificacionTecnicoCommand>
    {
        public ModificacionTecnicoValidator()
        {
            RuleFor(x => x.Persona)
                .NotNull();
            RuleFor(x => x.MediosDeContacto)
                .NotNull();
            RuleFor(x => x.AreaCoberturaLatitud)
                .NotNull();
            RuleFor(x => x.AreaCoberturaLongitud)
                .NotNull();
            RuleFor(x => x.AreaCoberturaRadio)
                .NotNull();
        }
    }
    
    public class ModificacionTecnicoHandler : IRequestHandler<ModificacionTecnicoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ModificacionTecnicoHandler> _logger;

        public ModificacionTecnicoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ModificacionTecnicoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Modificacion Tecnico - {Id}", request.Id);
            var validator = new ModificacionTecnicoValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Modificacion Tecnico - Datos invalidos");
                return Results.Problem();
            }
            
            var persona = _mapper.Map<Persona>(request.Persona);
            var mediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);
            var areaCobertura = new AreaCobertura(request.AreaCoberturaLatitud, request.AreaCoberturaLongitud, request.AreaCoberturaRadio);
            
            var tecnico = await _unitOfWork.TecnicoRepository.GetByIdAsync(request.Id);
            if (tecnico == null)
            {
                _logger.LogWarning("Modificacion Tecnico - Tecnico no encontrado - {Id}", request.Id);
                return Results.Problem("No se encontró el técnico");
            }
            
            persona.MediosDeContacto = mediosDeContacto;
            tecnico.Persona = persona;
            tecnico.AreaCobertura = areaCobertura;
            
            await _unitOfWork.PersonaRepository.UpdateAsync(tecnico.Persona);
            await _unitOfWork.AreaCoberturaRepository.UpdateAsync(tecnico.AreaCobertura);
            await _unitOfWork.TecnicoRepository.UpdateAsync(tecnico);
            await _unitOfWork.SaveChangesAsync();
            
            
            await _unitOfWork.SaveChangesAsync();
            return Results.Ok();
        }
    }
}