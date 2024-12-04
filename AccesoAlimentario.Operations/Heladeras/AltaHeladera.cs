using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class AltaHeladera
{
    public class AltaHeladeraCommand : IRequest<Heladera>
    {
        public PuntoEstrategicoRequest PuntoEstrategico { get; set; } = null!;
        public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
        public DateTime FechaInstalacion { get; set; } = DateTime.UtcNow;
        public float TemperaturaMinimaConfig { get; set; } = 0;
        public float TemperaturaMaximaConfig { get; set; } = 0;
        public List<SensorRequest> Sensores { get; set; } = [];
        public ModeloHeladeraRequest Modelo { get; set; } = null!;
    }

    public class AltaHeladeraValidator : AbstractValidator<AltaHeladeraCommand>
    {
        public AltaHeladeraValidator()
        {
            RuleFor(x => x.PuntoEstrategico)
                .NotNull();
            RuleFor(x => x.Estado)
                .NotNull();
            RuleFor(x => x.FechaInstalacion)
                .NotNull();
            RuleFor(x => x.TemperaturaMinimaConfig)
                .NotNull();
            RuleFor(x => x.TemperaturaMaximaConfig)
                .NotNull();
            RuleFor(x => x.Sensores)
                .NotNull();
            RuleFor(x => x.Modelo)
                .NotNull();
        }
    }

    public class Handler : IRequestHandler<AltaHeladeraCommand, Heladera>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Heladera> Handle(AltaHeladeraCommand request, CancellationToken cancellationToken)
        {
            var validator = new AltaHeladeraValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            var puntoEstrategico = _mapper.Map<PuntoEstrategico>(request.PuntoEstrategico);
            var modelo = _mapper.Map<ModeloHeladera>(request.Modelo);
            var sensores = _mapper.Map<List<Sensor>>(request.Sensores);
            
            var heladera = new Heladera
            {
                PuntoEstrategico = puntoEstrategico,
                Estado = request.Estado,
                FechaInstalacion = request.FechaInstalacion,
                TemperaturaMinimaConfig = request.TemperaturaMinimaConfig,
                TemperaturaMaximaConfig = request.TemperaturaMaximaConfig,
                Sensores = sensores,
                Modelo = modelo
            };
            
            await _unitOfWork.PuntoEstrategicoRepository.AddAsync(puntoEstrategico);
            await _unitOfWork.ModeloHeladeraRepository.AddAsync(modelo);
            await _unitOfWork.SensorRepository.AddRangeAsync(sensores);
            await _unitOfWork.HeladeraRepository.AddAsync(heladera);
            await _unitOfWork.SaveChangesAsync();
            
            return heladera;
        }
    }
}