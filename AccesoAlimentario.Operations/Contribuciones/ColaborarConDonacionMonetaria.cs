using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionMonetaria
{
    public class ColaborarConDonacionMonetariaCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
        public float Monto { get; set; }
        public int FrecuenciaDias { get; set; }
    }
    
    public class ColaborarConDonacionMonetariaHandler : IRequestHandler<ColaborarConDonacionMonetariaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ColaborarConDonacionMonetariaHandler(IUnitOfWork unitOfWork, ILogger<ColaborarConDonacionMonetariaHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ColaborarConDonacionMonetariaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Colaborar con donación monetaria - {request.ColaboradorId} - {request.FechaContribucion}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var donacion = new DonacionMonetaria
            {
                FechaContribucion = request.FechaContribucion,
                Monto = request.Monto,
                FrecuenciaDias = request.FrecuenciaDias
            };
            
            colaborador.AgregarContribucion(donacion);
            var appSettings = AppSettings.Instance;
            colaborador.AgregarPuntos(appSettings.PesoDonadosCoef * request.Monto);
            
            await _unitOfWork.DonacionMonetariaRepository.AddAsync(donacion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}