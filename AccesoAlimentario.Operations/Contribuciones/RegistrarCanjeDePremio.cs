using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class RegistrarCanjeDePremio
{
    public class RegistrarCanjeDePremioCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public Guid PremioId { get; set; }
    }
    
    public class RegistrarCanjeDePremioHandler : IRequestHandler<RegistrarCanjeDePremioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegistrarCanjeDePremioHandler> _logger;

        public RegistrarCanjeDePremioHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RegistrarCanjeDePremioHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(RegistrarCanjeDePremioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Registrar canje de premio - {request.ColaboradorId} - {request.PremioId}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var premio = await _unitOfWork.PremioRepository.GetByIdAsync(request.PremioId);
            if (premio == null)
            {
                _logger.LogWarning($"Premio no encontrado - {request.PremioId}");
                return Results.NotFound();
            }
            
            if (colaborador.Puntos < premio.PuntosNecesarios)
            {
                _logger.LogWarning($"No tiene suficientes puntos para canjear el premio - {colaborador.Puntos} - {premio.PuntosNecesarios}");
                return Results.BadRequest("No tiene suficientes puntos para canjear el premio");
            }

            premio.ReclamadoPor = colaborador;
            premio.FechaReclamo = DateTime.UtcNow;
            colaborador.Puntos -= premio.PuntosNecesarios;
            
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}