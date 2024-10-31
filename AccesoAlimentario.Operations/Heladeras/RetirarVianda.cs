using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class RetirarVianda
{
    public class RetirarViandaCommand : IRequest<IResult>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<RetirarViandaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public Handler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<IResult> Handle(RetirarViandaCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null || tarjeta is not TarjetaConsumo tarjetaConsumo)
            {
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
                return Results.Problem("No tiene permisos para retirar viandas de esta heladera");
            }
            
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            
            if (heladera == null)
            {
                return Results.NotFound("Heladera no encontrada");
            }
            
            if (heladera.ObtenerCantidadDeViandas() < 1)
            {
                return Results.Problem("No hay suficientes viandas disponibles");
            }

            var viandaConsumida = heladera.ConsumirVianda();
            var acceso = new AccesoHeladera
            {
                Tarjeta = tarjetaConsumo,
                Viandas = [viandaConsumida],
                FechaAcceso = DateTime.Now,
                TipoAcceso = TipoAcceso.RetiroVianda,
                Heladera = heladera
            };
            tarjetaConsumo.RegistrarAcceso(acceso);
            
            await _unitOfWork.AccesoHeladeraRepository.AddAsync(acceso);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok(viandaConsumida);
        }
    }
}