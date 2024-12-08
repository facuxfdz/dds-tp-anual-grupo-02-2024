using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Premios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConOfertaDePremio
{
    public class ColaborarConOfertaDePremioCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
        public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
        public string Nombre { get; set; } = string.Empty;
        public float PuntosNecesarios { get; set; } = 0;
        public string Imagen { get; set; } = string.Empty;
        public TipoRubro Rubro { get; set; }
    }
    
    public class ColaborarConOfertaDePremioHandler : IRequestHandler<ColaborarConOfertaDePremioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ColaborarConOfertaDePremioHandler> _logger;

        public ColaborarConOfertaDePremioHandler(IUnitOfWork unitOfWork, ILogger<ColaborarConOfertaDePremioHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ColaborarConOfertaDePremioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Colaborar con oferta de premio - {request.ColaboradorId} - {request.FechaContribucion}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var premio = new Premio
            {
                Nombre = request.Nombre,
                PuntosNecesarios = request.PuntosNecesarios,
                Imagen = request.Imagen,
                Rubro = request.Rubro
            };

            var contribucion = new OfertaPremio
            {
                FechaContribucion = request.FechaContribucion,
                Premio = premio
            };
            
            colaborador.AgregarContribucion(contribucion);
            
            await _unitOfWork.PremioRepository.AddAsync(premio);
            await _unitOfWork.OfertaPremioRepository.AddAsync(contribucion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}