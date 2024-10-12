using AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Externos;

public static class ObtenerRecomendacionUbicacionHeladera
{
    public class ObtenerRecomendacionUbicacionHeladeraCommand : IRequest<IResult>
    {
        public float Latitud { get; set; } = 0;
        public float Longitud { get; set; } = 0;
        public float Radio { get; set; } = 0;
    }

    public class Handler : IRequestHandler<ObtenerRecomendacionUbicacionHeladeraCommand, IResult>
    {
        public Handler()
        {
        }

        public async Task<IResult> Handle(ObtenerRecomendacionUbicacionHeladeraCommand request,
            CancellationToken cancellationToken)
        {
            var recomendador = new ConsultoraExternaApi();
            var recomendaciones = await recomendador.GetRecomendacion(request.Latitud, request.Longitud, request.Radio);
            return Results.Ok(recomendaciones);
        }
    }
}