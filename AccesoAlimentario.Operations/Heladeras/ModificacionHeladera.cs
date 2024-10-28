using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ModificacionHeladera
{
    public class ModificacionHeladeraCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public PuntoEstrategicoRequest PuntoEstrategico { get; set; } = null!;
        public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
        public float TemperaturaMinimaConfig { get; set; } = 0;
        public float TemperaturaMaximaConfig { get; set; } = 0;
        public List<SensorRequest> Sensores { get; set; } = [];
    }
    
    public class ModificacionHeladeraValidator : AbstractValidator<ModificacionHeladeraCommand>
    {
        public ModificacionHeladeraValidator()
        {
            RuleFor(x => x.PuntoEstrategico)
                .NotNull();
            RuleFor(x => x.Estado)
                .NotNull();
            RuleFor(x => x.TemperaturaMinimaConfig)
                .NotNull();
            RuleFor(x => x.TemperaturaMaximaConfig)
                .NotNull();
            RuleFor(x => x.Sensores)
                .NotNull();
        }
    }
    
    public class Handler : IRequestHandler<ModificacionHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool IsDataValid(Heladera h)
        {
            if (h.TemperaturaMinimaConfig < h.Modelo.TemperaturaMinima ||
                h.TemperaturaMaximaConfig > h.Modelo.TemperaturaMaxima)
            {
                return false;
            }
            return true;
        }

        public async Task<IResult> Handle(ModificacionHeladeraCommand request, CancellationToken cancellationToken)
        {
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                return Results.NotFound("La heladera no existe");
            }
            
            if (!IsDataValid(heladera))
            {
                return Results.BadRequest("Parametros fuera de los valores permitidos");
            }
            
            await _unitOfWork.HeladeraRepository.UpdateAsync(heladera);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}
