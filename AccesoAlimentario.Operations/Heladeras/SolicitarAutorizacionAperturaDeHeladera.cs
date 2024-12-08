using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Tarjetas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class SolicitarAutorizacionAperturaDeHeladera
{
    public class SolicitarAutorizacionAperturaDeHeladeraCommand : IRequest<IResult>
    {
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public Guid TarjetaId { get; set; } = Guid.Empty;
    }
    
    public class SolicitarAutorizacionAperturaDeHeladeraHandler : IRequestHandler<SolicitarAutorizacionAperturaDeHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SolicitarAutorizacionAperturaDeHeladeraHandler> _logger;

        public SolicitarAutorizacionAperturaDeHeladeraHandler(IUnitOfWork unitOfWork, ILogger<SolicitarAutorizacionAperturaDeHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(SolicitarAutorizacionAperturaDeHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Solicitar autorización de apertura de heladera - {request.HeladeraId}");
            var tarjeta = await _unitOfWork.TarjetaRepository.GetByIdAsync(request.TarjetaId);
            if (tarjeta == null || tarjeta is not TarjetaColaboracion tarjetaColaboracion)
            {
                _logger.LogWarning($"Tarjeta no encontrada - {request.TarjetaId}");
                return Results.NotFound("Tarjeta no encontrada");
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.HeladeraId}");
                return Results.NotFound("Heladera no encontrada");
            }

            var auth = new AutorizacionManipulacionHeladera
            {
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddHours(3),
                Heladera = heladera,
                TarjetaAutorizada = tarjetaColaboracion
            };
            
            await _unitOfWork.AutorizacionManipulacionHeladeraRepository.AddAsync(auth);
            tarjetaColaboracion.Autorizaciones.Add(auth);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}