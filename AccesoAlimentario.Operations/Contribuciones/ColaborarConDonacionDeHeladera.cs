using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AccesoAlimentario.Operations.Heladeras;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionDeHeladera
{
    public class ColaborarConDonacionDeHeladeraCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
        public DateTime FechaContribucion { get; set; } = DateTime.Now;
        public PuntoEstrategicoRequest PuntoEstrategico { get; set; } = null!;
        public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
        public DateTime FechaInstalacion { get; set; } = DateTime.Now;
        public float TemperaturaMinimaConfig { get; set; } = 0;
        public float TemperaturaMaximaConfig { get; set; } = 0;
        public List<SensorRequest> Sensores { get; set; } = [];
        public ModeloHeladeraRequest Modelo { get; set; } = null!;
    }

    public class Handler : IRequestHandler<ColaborarConDonacionDeHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public Handler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        
        public async Task<IResult> Handle(ColaborarConDonacionDeHeladeraCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var altaHeladeraCommand = new AltaHeladera.AltaHeladeraCommand
            {
                PuntoEstrategico = request.PuntoEstrategico,
                Estado = request.Estado,
                FechaInstalacion = request.FechaInstalacion,
                TemperaturaMinimaConfig = request.TemperaturaMinimaConfig,
                TemperaturaMaximaConfig = request.TemperaturaMaximaConfig,
                Sensores = request.Sensores,
                Modelo = request.Modelo
            };
            var heladera = await _mediator.Send(altaHeladeraCommand, cancellationToken);

            var contribucion = new AdministracionHeladera
            {
                FechaContribucion = request.FechaContribucion,
                Heladera = heladera,
            };

            colaborador.AgregarContribucion(contribucion);
            await _unitOfWork.AdministracionHeladeraRepository.AddAsync(contribucion);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok(contribucion);
        }
    }
}