using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AccesoAlimentario.Operations.Heladeras;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionDeHeladera
{
    public class ColaborarConDonacionDeHeladeraCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
        public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
        public PuntoEstrategicoRequest PuntoEstrategico { get; set; } = null!;
        public EstadoHeladera Estado { get; set; } = EstadoHeladera.FueraServicio;
        public DateTime FechaInstalacion { get; set; } = DateTime.UtcNow;
        public float TemperaturaMinimaConfig { get; set; } = 0;
        public float TemperaturaMaximaConfig { get; set; } = 0;
        public List<SensorRequest> Sensores { get; set; } = [];
        public ModeloHeladeraRequest Modelo { get; set; } = null!;
    }

    public class ColaborarConDonacionDeHeladeraHandler : IRequestHandler<ColaborarConDonacionDeHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<ColaborarConDonacionDeHeladeraHandler> _logger;
        
        public ColaborarConDonacionDeHeladeraHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<ColaborarConDonacionDeHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(ColaborarConDonacionDeHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Colaborar con donación de heladera - {request.ColaboradorId} - {request.FechaContribucion}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
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
            
            return Results.Ok();
        }
    }
}