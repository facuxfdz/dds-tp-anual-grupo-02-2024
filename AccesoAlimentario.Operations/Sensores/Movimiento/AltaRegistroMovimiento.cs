using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Sensores;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
    
    public class AltaRegistroMovimientoHandler : IRequestHandler<AltaRegistroMovimientoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AltaRegistroMovimientoHandler> _logger;
        
        public AltaRegistroMovimientoHandler(IUnitOfWork unitOfWork, ILogger<AltaRegistroMovimientoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(AltaRegistroMovimientoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Alta registro movimiento - {request.SensorId} - {request.Fecha}");
            var sensor = await _unitOfWork.SensorRepository.GetByIdAsync(request.SensorId);
            if (sensor == null || sensor is not SensorMovimiento sensorMovimiento)
            {
                _logger.LogWarning($"Sensor no encontrado - {request.SensorId}");
                return Results.NotFound("Sensor no encontrado");
            }
            
            var registroId = sensorMovimiento.Registrar(request.Fecha, request.Movimiento);
            if (registroId == Guid.Empty)
            {
                _logger.LogWarning("Error al registrar el movimiento");
                return Results.BadRequest("Error al registrar el movimiento");
            }
            var registro = sensorMovimiento.RegistrosMovimiento.Find(r => r.Id == registroId);
            if (registro == null)
            {
                _logger.LogWarning("Error al registrar el movimiento");
                return Results.BadRequest("Error al registrar el movimiento");
            }
            
            await _unitOfWork.RegistroMovimientoRepository.AddAsync(registro);
            await _unitOfWork.SensorRepository.UpdateAsync(sensorMovimiento);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}