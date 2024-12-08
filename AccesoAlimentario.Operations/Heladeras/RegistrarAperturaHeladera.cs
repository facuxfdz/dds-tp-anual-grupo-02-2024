using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class RegistrarAperturaHeladera
{
    public class RegistrarAperturaHeladeraCommand : IRequest<IResult>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    }

    public class RegistrarAperturaHeladeraHandler : IRequestHandler<RegistrarAperturaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<RegistrarAperturaHeladeraHandler> _logger;

        public RegistrarAperturaHeladeraHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<RegistrarAperturaHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IResult> Handle(RegistrarAperturaHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Registrar apertura de heladera - {request.HeladeraId}");
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null || tarjeta is not TarjetaColaboracion tarjetaColaboracion)
            {
                _logger.LogWarning($"Tarjeta no encontrada - {request.TarjetaId}");
                return Results.NotFound("Tarjeta no encontrada");
            }

            var validarCommand = new ValidarAccesoHeladera.ValidarAccesoHeladeraCommand
            {
                TarjetaId = request.TarjetaId,
                HeladeraId = request.HeladeraId
            };
            var puedeAcceder = await _mediator.Send(validarCommand, cancellationToken);

            if (!puedeAcceder)
            {
                _logger.LogWarning($"No tiene permisos para abrir esta heladera - {request.HeladeraId}");
                return Results.Problem("No tiene permisos para abrir esta heladera");
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);

            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.HeladeraId}");
                return Results.NotFound("Heladera no encontrada");
            }

            var auth = tarjetaColaboracion.TieneAutorizacion(heladera);
            if (auth == null)
            {
                _logger.LogWarning($"No tiene autorización para abrir esta heladera - {request.HeladeraId}");
                return Results.Problem("No tiene autorización para abrir esta heladera");
            }

            var acceso = new AccesoHeladera
            {
                Tarjeta = tarjetaColaboracion,
                Heladera = heladera,
                FechaAcceso = DateTime.UtcNow,
                Autorizacion = auth,
                TipoAcceso = request.TipoAcceso
            };
            await _unitOfWork.AccesoHeladeraRepository.AddAsync(acceso);
            tarjetaColaboracion.Accesos.Add(acceso);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}