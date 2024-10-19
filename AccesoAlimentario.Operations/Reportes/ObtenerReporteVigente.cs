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
            var creador = new CreadorDeReportes();
            
            // TODO: Revisar el uso de la variable creador
            
            var reporte = creador.ObtenerReporteVigente(request.TipoReporte);
            
            return Results.Ok(reporte);
        }
    }
}