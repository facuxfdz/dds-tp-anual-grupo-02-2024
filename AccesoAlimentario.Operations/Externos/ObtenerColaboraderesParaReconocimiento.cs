using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;
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

            var colaboradoresValidos = new List<Colaborador>();
            foreach (var colaborador in colaboradores)
            {
                var donacionesViandas = colaborador.ContribucionesRealizadas.OfType<DonacionVianda>().ToList();
                var cantidadDonadaUltimoMes = donacionesViandas.Count(d => d.FechaContribucion >= DateTime.Now.AddDays(-30));
                if (cantidadDonadaUltimoMes >= request.DonacionesViandasMinimas)
                {
                    colaboradoresValidos.Add(colaborador);
                }
            }

            var response = colaboradoresValidos
                .OrderBy(c => c.Puntos)
                .Take(request.CantidadDeColaboradores)
                .ToList();

            return Results.Ok(response);
        }
    }
}