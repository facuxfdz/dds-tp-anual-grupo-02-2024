using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class RegistrarAperturaHeladera
{
    public class RegistrarAperturaHeladeraCommand : IRequest<IResult>
    {
        public Guid TarjetaId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    }

    public class Handler : IRequestHandler<RegistrarAperturaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public Handler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<IResult> Handle(RegistrarAperturaHeladeraCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null || tarjeta is not TarjetaColaboracion tarjetaColaboracion)
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
                return Results.Problem("No tiene permisos para abrir esta heladera");
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);

            if (heladera == null)
            {
                return Results.NotFound("Heladera no encontrada");
            }

            var auth = tarjetaColaboracion.TieneAutorizacion(heladera);
            if (auth == null)
            {
                return Results.Problem("No tiene autorización para abrir esta heladera");
            }

            var acceso = new AccesoHeladera
            {
                Tarjeta = tarjetaColaboracion,
                Heladera = heladera,
                FechaAcceso = DateTime.Now,
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