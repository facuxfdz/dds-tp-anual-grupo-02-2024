using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Operations.Dto.Responses.SuscripcionesColaboradores;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public class ObtenerSuscripciones
{
    public class ObtenerSuscripcionesQuery : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
    }

    internal class ObtenerSuscripcionesHandler : IRequestHandler<ObtenerSuscripcionesQuery, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerSuscripcionesHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ObtenerSuscripcionesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerSuscripcionesQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo suscripciones de colaborador");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var suscripciones = colaborador.Suscripciones;
            var response = suscripciones.Select(s => _mapper.Map(s, typeof(Suscripcion), typeof(SuscripcionResponse)));
            return Results.Ok(response);
        }
    }
}