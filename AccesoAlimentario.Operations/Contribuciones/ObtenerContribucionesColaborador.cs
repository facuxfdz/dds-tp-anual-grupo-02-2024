using AccesoAlimentario.Core.DAL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ObtenerContribucionesColaborador
{
    public class ObtenerContribucionesColaboradorCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
    }
    
    public class ObtenerContribucionesColaboradorHandler : IRequestHandler<ObtenerContribucionesColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ObtenerContribucionesColaboradorHandler(IUnitOfWork unitOfWork, ILogger<ObtenerContribucionesColaboradorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerContribucionesColaboradorCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Contribuciones Colaborador");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(colaborador.ContribucionesRealizadas);
        }
    }
}