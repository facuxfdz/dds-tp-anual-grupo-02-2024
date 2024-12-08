using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Sensores;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Sensores.Temperatura;

public static class AltaRegistroTemperatura
{
    public class AltaRegistroTemperaturaCommand : IRequest<IResult>
    {
        public Guid SensorId { get; set; } = Guid.Empty;
        public DateTime Fecha { get; set; }
        public string Temperatura { get; set; } = string.Empty;
    }
    
    public class AltaRegistroTemperaturaValidator : AbstractValidator<AltaRegistroTemperaturaCommand>
    {
        public AltaRegistroTemperaturaValidator()
        {
            RuleFor(x => x.SensorId)
                .NotNull();
            RuleFor(x => x.Fecha)
                .NotNull();
            RuleFor(x => x.Temperatura)
                .NotNull();
        }
    }
    
    public class AltaRegistroTemperaturaHandler : IRequestHandler<AltaRegistroTemperaturaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AltaRegistroTemperaturaHandler> _logger;
        
        public AltaRegistroTemperaturaHandler(IUnitOfWork unitOfWork, ILogger<AltaRegistroTemperaturaHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(AltaRegistroTemperaturaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Alta registro temperatura - {request.SensorId} - {request.Fecha}");
            var sensor = await _unitOfWork.SensorRepository.GetByIdAsync(request.SensorId);
            if (sensor == null || sensor is not SensorTemperatura sensorTemperatura)
            {
                _logger.LogWarning($"Sensor no encontrado - {request.SensorId}");
                return Results.NotFound("Sensor no encontrado");
            }
            
            var id = sensorTemperatura.Registrar(request.Fecha, request.Temperatura);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Error al registrar la temperatura");
                return Results.BadRequest("Error al registrar la temperatura");
            }
            var registro = sensorTemperatura.RegistrosTemperatura.Find(r => r.Id == id);
            if (registro == null)
            {
                _logger.LogWarning("Error al registrar la temperatura");
                return Results.BadRequest("Error al registrar la temperatura");
            }
            await _unitOfWork.RegistroTemperaturaRepository.AddAsync(registro);
            await _unitOfWork.SensorRepository.UpdateAsync(sensorTemperatura);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}