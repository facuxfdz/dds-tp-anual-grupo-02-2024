using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using MediatR;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Common;

namespace AccesoAlimentario.Operations.Externos;

public static class ObtenerColaboraderesParaReconocimiento
{
    public class ObtenerColaboraderesParaReconocimientoCommand : IRequest<IResult>
    {
        public int PuntosMinimos { get; set; } = 0;
        public int DonacionesViandasMinimas { get; set; } = 0;
        public int CantidadDeColaboradores { get; set; } = 0;
    }

    public class Handler : IRequestHandler<ObtenerColaboraderesParaReconocimientoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(ObtenerColaboraderesParaReconocimientoCommand request,
            CancellationToken cancellationToken)
        {
            var query = _unitOfWork.ColaboradorRepository.GetQueryable();
            query = query.Where(c => c.Puntos >= request.PuntosMinimos);
            var colaboradores = await _unitOfWork.ColaboradorRepository.GetCollectionAsync(query);
            colaboradores = colaboradores.Where(
                c => c.ContribucionesRealizadas.Any(d => d.FechaContribucion >= DateTime.Now.AddDays(-30))
            );
            var colaboradoresValidos = colaboradores.Where(
                c => c.ContribucionesRealizadas.OfType<DonacionVianda>().Count() >= request.DonacionesViandasMinimas);

            colaboradoresValidos = colaboradoresValidos.OrderBy(c => c.Puntos).Take(request.CantidadDeColaboradores);

            var response = colaboradoresValidos.Take(request.CantidadDeColaboradores).ToList();

            return Results.Ok(response);
        }
    }
}