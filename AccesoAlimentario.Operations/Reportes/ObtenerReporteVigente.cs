using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Reportes;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Reportes;

public static class ObtenerReporteVigente
{
    public class ObtenerReporteVigenteCommand : IRequest<IResult>
    {
        public TipoReporte TipoReporte { get; set; }
    }

    public class Handler : IRequestHandler<ObtenerReporteVigenteCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(ObtenerReporteVigenteCommand request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.ReporteRepository.GetQueryable();
            query = query
                .Where(r => r.Tipo == request.TipoReporte)
                .Where(r => r.FechaExpiracion < DateTime.Now);

            var reporte = await _unitOfWork.ReporteRepository.GetAsync(query);

            if (reporte == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(reporte);
        }
    }
}