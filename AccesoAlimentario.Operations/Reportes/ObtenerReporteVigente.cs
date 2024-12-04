using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Dto.Responses.Reportes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Reportes;

public static class ObtenerReporteVigente
{
    public class ObtenerReporteVigenteCommand : IRequest<IResult>
    {
        public TipoReporte TipoReporte { get; set; }
    }

    public class ObtenerReporteVigenteHandler : IRequestHandler<ObtenerReporteVigenteCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerReporteVigenteHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerReporteVigenteHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerReporteVigenteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo reporte vigente");
            var query = _unitOfWork.ReporteRepository.GetQueryable();
            query = query
                .Where(r => r.Tipo == request.TipoReporte)
                .Where(r => r.FechaExpiracion < DateTime.Now);

            var reporte = await _unitOfWork.ReporteRepository.GetAsync(query);

            if (reporte == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(_mapper.Map(reporte, reporte.GetType(), typeof(ReporteResponse)));
        }
    }
}