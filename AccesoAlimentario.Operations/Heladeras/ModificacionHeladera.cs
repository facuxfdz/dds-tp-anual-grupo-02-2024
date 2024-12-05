using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;
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
        public ModeloHeladeraRequest Modelo { get; set; } = null!;
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

        public async Task<IResult> Handle(ModificacionHeladeraCommand request, CancellationToken cancellationToken)
        {
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                return Results.NotFound("La heladera no existe");
            }

            var puntoEstrategico = _mapper.Map<PuntoEstrategico>(request.PuntoEstrategico);
            var modelo = _mapper.Map<ModeloHeladera>(request.Modelo);
            var sensores = _mapper.Map<List<Sensor>>(request.Sensores);

            heladera.PuntoEstrategico.Actualizar(puntoEstrategico.Nombre, puntoEstrategico.Longitud,
                puntoEstrategico.Latitud, puntoEstrategico.Direccion);

            heladera.Estado = request.Estado;
            
            heladera.Modelo.Actualizar(modelo.Capacidad, modelo.TemperaturaMinima, modelo.TemperaturaMaxima);
            
            heladera.CambiarTemperaturaMaxima(request.TemperaturaMaximaConfig);
            heladera.CambiarTemperaturaMinima(request.TemperaturaMinimaConfig);

            foreach (var sensor in sensores)
            {
                var sensorHeladera = heladera.Sensores.FirstOrDefault(s => s.Id == sensor.Id);
                if (sensorHeladera == null)
                {
                    heladera.AgregarSensor(sensor);
                }
            }

            await _unitOfWork.HeladeraRepository.UpdateAsync(heladera);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}