using AccesoAlimentario.Core.DAL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerPuntajeColaborador
{
    public class ObtenerPuntajeColaboradorCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
    }
    
    public class ObtenerPuntajeColaboradorHandler : IRequestHandler<ObtenerPuntajeColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ObtenerPuntajeColaboradorHandler(IUnitOfWork unitOfWork, ILogger<ObtenerPuntajeColaboradorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerPuntajeColaboradorCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Puntaje Colaborador - {ColaboradorId}", request.ColaboradorId);
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador no encontrado - {ColaboradorId}", request.ColaboradorId);
                return Results.NotFound();
            }

            return Results.Ok(colaborador.Puntos);
        }
    }
}