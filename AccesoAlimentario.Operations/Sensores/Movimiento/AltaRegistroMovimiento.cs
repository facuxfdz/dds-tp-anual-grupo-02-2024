using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Sensores;
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
            if (sensor == null || sensor is not SensorMovimiento sensorMovimiento)
            {
                return Results.NotFound("Sensor no encontrado");
            }
            
            var registroId = sensorMovimiento.Registrar(request.Fecha, request.Movimiento);
            if (registroId == Guid.Empty)
            {
                return Results.BadRequest("Error al registrar el movimiento");
            }
            var registro = sensorMovimiento.RegistrosMovimiento.Find(r => r.Id == registroId);
            if (registro == null)
            {
                return Results.BadRequest("Error al registrar el movimiento");
            }
            
            await _unitOfWork.RegistroMovimientoRepository.AddAsync(registro);
            await _unitOfWork.SensorRepository.UpdateAsync(sensorMovimiento);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}