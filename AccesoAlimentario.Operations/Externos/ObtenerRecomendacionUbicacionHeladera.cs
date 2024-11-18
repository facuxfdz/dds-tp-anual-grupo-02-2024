using AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Externos;

public static class ObtenerRecomendacionUbicacionHeladera
{
    public class ObtenerRecomendacionUbicacionHeladeraCommand : IRequest<IResult>
    {
        public float Latitud { get; set; } = 0;
        public float Longitud { get; set; } = 0;
        public float Radio { get; set; } = 0;
    }

    public class ObtenerRecomendacionUbicacionHeladeraHandler : IRequestHandler<ObtenerRecomendacionUbicacionHeladeraCommand, IResult>
    {
        private readonly ILogger _logger;
        public ObtenerRecomendacionUbicacionHeladeraHandler(ILogger<ObtenerRecomendacionUbicacionHeladeraHandler> logger)
        {
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerRecomendacionUbicacionHeladeraCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo recomendacion de ubicacion de heladeras");
            var recomendador = new ConsultoraExternaApi();
            var recomendaciones = await recomendador.GetRecomendacion(request.Latitud, request.Longitud, request.Radio);
            return Results.Ok(recomendaciones);
        }
    }
}