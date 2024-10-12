using AccesoAlimentario.Core.DAL;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Sensores.Movimiento;

public static class AltaRegistroMovimiento
{
    public class AltaRegistroMovimientoCommand : IRequest<IResult>
    {
        public Guid SensorId { get; set; } = Guid.Empty;
        public DateTime Fecha { get; set; }
        public string Movimiento { get; set; } = string.Empty;
    }
    
    public class AltaRegistroMovimientoValidator : AbstractValidator<AltaRegistroMovimientoCommand>
    {
        public AltaRegistroMovimientoValidator()
        {
            RuleFor(x => x.SensorId)
                .NotNull();
            RuleFor(x => x.Fecha)
                .NotNull();
            RuleFor(x => x.Movimiento)
                .NotNull();
        }
    }
    
    public class Handler : IRequestHandler<AltaRegistroMovimientoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IResult> Handle(AltaRegistroMovimientoCommand request, CancellationToken cancellationToken)
        {
            var sensor = await _unitOfWork.SensorRepository.GetByIdAsync(request.SensorId);
            if (sensor == null)
            {
                return Results.NotFound("Sensor no encontrado");
            }
            
            sensor.Registrar(request.Fecha, request.Movimiento);

            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}