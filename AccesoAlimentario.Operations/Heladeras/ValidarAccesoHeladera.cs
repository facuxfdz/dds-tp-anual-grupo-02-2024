using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ValidarAccesoHeladera
{
    public class ValidarAccesoHeladeraCommand : IRequest<bool>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<ValidarAccesoHeladeraCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ValidarAccesoHeladeraCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null)
            {
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
                    return false;
                }

                return tarjetaColaboracion.TieneAutorizacion(heladera) != null;
            }

            return false;
        }
    }
}