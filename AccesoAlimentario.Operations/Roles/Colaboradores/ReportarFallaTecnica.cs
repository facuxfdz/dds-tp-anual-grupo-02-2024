using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ReportarFallaTecnica
{
    public class ReportarFallaTecnicaCommand : IRequest<IResult>
    {
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public Guid ReporteroId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
    }
    
    public class ReportarFallaTecnicaHandler : IRequestHandler<ReportarFallaTecnicaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ReportarFallaTecnicaHandler(IUnitOfWork unitOfWork, ILogger<ReportarFallaTecnicaHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ReportarFallaTecnicaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Reportar Falla Tecnica");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ReporteroId);

            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador no encontrado - {ReporteroId}", request.ReporteroId);
                return Results.NotFound("Colaborador no encontrado");
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);

            if (heladera == null)
            {
                _logger.LogWarning("Heladera no encontrada - {HeladeraId}", request.HeladeraId);
                return Results.NotFound("Heladera no encontrada");
            }

            var fallaTecnica = new FallaTecnica
            {
                Fecha = request.Fecha,
                Colaborador = colaborador,
                Descripcion = request.Descripcion,
                Foto = request.Foto
            };

            heladera.AgregarIncidente(fallaTecnica);

            await _unitOfWork.IncidenteRepository.AddAsync(fallaTecnica);

            var tecnicosQuery = _unitOfWork.TecnicoRepository.GetQueryable();
            var tecnicos = (await _unitOfWork.TecnicoRepository.GetCollectionAsync(tecnicosQuery)).ToList();

            if (tecnicos.Count > 0)
            {
                var tecnicoMasCeracno = tecnicos.OrderBy(t => t.ObtenerDistancia(heladera)).First();
                tecnicoMasCeracno.NotificarIncidente(fallaTecnica);
            }
            
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}