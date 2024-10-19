using AccesoAlimentario.Core.DAL;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

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
    
    public class Handler : IRequestHandler<AltaRegistroTemperaturaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IResult> Handle(AltaRegistroTemperaturaCommand request, CancellationToken cancellationToken)
        {
            var sensor = await _unitOfWork.SensorRepository.GetByIdAsync(request.SensorId);
            if (sensor == null)
            {
                return Results.NotFound("Sensor no encontrado");
            }
            
            sensor.Registrar(request.Fecha, request.Temperatura);

            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}