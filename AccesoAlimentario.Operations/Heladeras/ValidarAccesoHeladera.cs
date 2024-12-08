using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ValidarAccesoHeladera
{
    public class ValidarAccesoHeladeraCommand : IRequest<bool>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
    }
    
    public class ValidarAccesoHeladeraHandler : IRequestHandler<ValidarAccesoHeladeraCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ValidarAccesoHeladeraHandler> _logger;

        public ValidarAccesoHeladeraHandler(IUnitOfWork unitOfWork, ILogger<ValidarAccesoHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(ValidarAccesoHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Validar acceso a heladera - {request.HeladeraId}");
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null)
            {
                _logger.LogWarning($"Tarjeta no encontrada - {request.TarjetaId}");
                return false;
            }
            
            if (tarjeta is TarjetaConsumo tarjetaConsumo)
            {
                return tarjetaConsumo.PuedeConsumir();
            } 
            if (tarjeta is TarjetaColaboracion tarjetaColaboracion)
            {
                var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
                if (heladera == null)
                {
                    _logger.LogWarning($"Heladera no encontrada - {request.HeladeraId}");
                    return false;
                }

                return tarjetaColaboracion.TieneAutorizacion(heladera) != null;
            }

            return false;
        }
    }
}