using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ModificacionColaborador
{
    public class ModificacionColaboradorCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public PersonaRequest Persona { get; set; } = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
    }
    
    public class ModificacionColaboradorValidator : AbstractValidator<ModificacionColaboradorCommand>
    {
        public ModificacionColaboradorValidator()
        {
            RuleFor(x => x.Persona)
                .NotNull();
            RuleFor(x => x.MediosDeContacto)
                .NotNull();
        }
    }
    
    public class ModificacionColaboradorHandler : IRequestHandler<ModificacionColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ModificacionColaboradorHandler> _logger;

        public ModificacionColaboradorHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ModificacionColaboradorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ModificacionColaboradorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Modificación de colaborador - {Id}", request.Id);
            var validator = new ModificacionColaboradorValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Datos de modificación de colaborador inválidos");
                return Results.Problem();
            }
            
            var persona = _mapper.Map<Persona>(request.Persona);
            var mediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);
            
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);
            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador no encontrado - {Id}", request.Id);
                return Results.NotFound();
            }

            persona.MediosDeContacto = mediosDeContacto;
            colaborador.Persona = persona;
            
            await _unitOfWork.ColaboradorRepository.UpdateAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
    
}