using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Operations.Dto.Responses.Premios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ObtenerPremios
{
    public class ObtenerPremiosQuery : IRequest<IResult>
    {
        public string? Nombre { get; set; } = null;
        public float? PuntosNecesarios { get; set; } = null;
        public TipoRubro? Rubro { get; set; }
    }
    
    internal class ObtenerPremiosHandler : IRequestHandler<ObtenerPremiosQuery, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerPremiosHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerPremiosHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerPremiosQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obtener premios - {request.Nombre} - {request.PuntosNecesarios}");
            var query = _unitOfWork.PremioRepository.GetQueryable();
            query = query.Where(p => p.ReclamadoPor == null);
            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(p => p.Nombre.Contains(request.Nombre));
            }
            if (request.PuntosNecesarios.HasValue)
            {
                query = query.Where(p => p.PuntosNecesarios <= request.PuntosNecesarios);
            }
            if (request.Rubro.HasValue)
            {
                query = query.Where(p => p.Rubro == request.Rubro);
            }

            var premios = await _unitOfWork.PremioRepository.GetCollectionAsync(query);
            var response = premios.Select(p => _mapper.Map<PremioResponse>(p));
            return Results.Ok(response);
        }
    }
}