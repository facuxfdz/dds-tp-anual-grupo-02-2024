using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class RetirarVianda
{
    public class RetirarViandaCommand : IRequest<IResult>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
    }
    
    public class RetirarViandaHandler : IRequestHandler<RetirarViandaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<RetirarViandaHandler> _logger;

        public RetirarViandaHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<RetirarViandaHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IResult> Handle(RetirarViandaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retirar vianda de heladera - {request.HeladeraId}");
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null)
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
                _logger.LogWarning($"No tiene permisos para retirar viandas de esta heladera - {request.HeladeraId}");
                return Results.Problem("No tiene permisos para retirar viandas de esta heladera");
            }
            
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            
            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.HeladeraId}");
                return Results.NotFound("Heladera no encontrada");
            }
            
            if (heladera.ObtenerCantidadDeViandas() < 1)
            {
                _logger.LogWarning($"No hay suficientes viandas disponibles en la heladera - {request.HeladeraId}");
                return Results.Problem("No hay suficientes viandas disponibles");
            }

            var viandaConsumida = heladera.ConsumirVianda();
            var acceso = new AccesoHeladera
            {
                Tarjeta = tarjeta,
                Viandas = [viandaConsumida],
                FechaAcceso = DateTime.UtcNow,
                TipoAcceso = TipoAcceso.RetiroVianda,
                Heladera = heladera,
                Autorizacion = tarjeta is TarjetaColaboracion tarjetaColaboracion ? tarjetaColaboracion.TieneAutorizacion(heladera) : null
            };
            tarjeta.RegistrarAcceso(acceso);
            
            await _unitOfWork.AccesoHeladeraRepository.AddAsync(acceso);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}