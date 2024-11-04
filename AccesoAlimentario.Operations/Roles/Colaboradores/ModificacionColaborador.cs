using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

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
    
    public class Handler : IRequestHandler<ModificacionColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ModificacionColaboradorCommand request, CancellationToken cancellationToken)
        {
            var validator = new ModificacionColaboradorValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Results.Problem();
            }
            
            var persona = _mapper.Map<Persona>(request.Persona);
            var mediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);
            
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);
            if (colaborador == null)
            {
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