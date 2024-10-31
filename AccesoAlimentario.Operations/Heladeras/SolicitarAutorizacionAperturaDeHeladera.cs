using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class SolicitarAutorizacionAperturaDeHeladera
{
    public class SolicitarAutorizacionAperturaDeHeladeraCommand : IRequest<IResult>
    {
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public Guid TarjetaId { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<SolicitarAutorizacionAperturaDeHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(SolicitarAutorizacionAperturaDeHeladeraCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null || tarjeta is not TarjetaColaboracion tarjetaColaboracion)
            {
                return Results.NotFound("Tarjeta no encontrada");
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            if (heladera == null)
            {
                return Results.NotFound("Heladera no encontrada");
            }

            var auth = new AutorizacionManipulacionHeladera
            {
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddHours(3),
                Heladera = heladera,
                TarjetaAutorizada = tarjetaColaboracion
            };
            
            await _unitOfWork.AutorizacionManipulacionHeladeraRepository.AddAsync(auth);
            tarjetaColaboracion.Autorizaciones.Add(auth);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok(auth);
        }
    }
}