using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Dto.Responses.Contribuciones;
using AutoMapper;
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

    public class
        ObtenerContribucionesColaboradorHandler : IRequestHandler<ObtenerContribucionesColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ObtenerContribucionesColaboradorHandler(IUnitOfWork unitOfWork,
            ILogger<ObtenerContribucionesColaboradorHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ObtenerContribucionesColaboradorCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obtener contribuciones de colaborador - {request.ColaboradorId}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var response = colaborador.ContribucionesRealizadas
                .Select(c => _mapper.Map(c, c.GetType(), typeof(FormaContribucionResponse)));
            
            return Results.Ok(response);
        }
    }
}