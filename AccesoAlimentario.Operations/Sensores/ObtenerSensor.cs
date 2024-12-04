using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Dto.Responses.Sensores;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Sensores;

public static class ObtenerSensor
{
    public class ObtenerSensorCommand : IRequest<IResult>
    {
        public Guid SensorId { get; set; }
    }

    internal class ObtenerSensorHandler : IRequestHandler<ObtenerSensorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerSensorHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerSensorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerSensorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo sensor con id {SensorId}", request.SensorId);
            var sensor = await _unitOfWork.SensorRepository.GetByIdAsync(request.SensorId);

            if (sensor == null)
            {
                return Results.BadRequest("Sensor no encontrado");
            }

            var response = _mapper.Map(sensor, sensor.GetType(), typeof(SensorResponse));
            return Results.Ok(response);
        }
    }
}