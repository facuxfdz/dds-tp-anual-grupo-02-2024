using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;


namespace AccesoAlimentario.Operations.Externos;

public static class ObtenerColaboraderesParaReconocimiento
{
    public class ObtenerColaboraderesParaReconocimientoCommand : IRequest<IResult>
    {
        /// <summary>
        /// Cantidad mínima de puntos que debe tener el colaborador para ser considerado.
        /// </summary>
        [SwaggerSchema("Cantidad mínima de puntos.")]
        public int PuntosMinimos { get; set; } = 0;
        /// <summary>
        /// Número mínimo de donaciones de viandas realizadas en el último mes.
        /// </summary>
        [SwaggerSchema("Donaciones de viandas mínimas en el último mes.")]
        public int DonacionesViandasMinimas { get; set; } = 0;
        /// <summary>
        /// Número máximo de colaboradores a retornar.
        /// </summary>
        [SwaggerSchema("Cantidad de colaboradores a retornar.")]
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
                .Select(c => new ColaboradorResponse
                {
                    Id = c.Id.ToString(),
                    Nombre = c.Persona.Nombre,
                    Puntos = c.Puntos,
                    DonacionesUltimoMes = c.ContribucionesRealizadas.OfType<DonacionVianda>()
                        .Count(d => d.FechaContribucion >= DateTime.Now.AddDays(-30))
                })
                .ToList();

            return Results.Ok(response);

        }
    }
}