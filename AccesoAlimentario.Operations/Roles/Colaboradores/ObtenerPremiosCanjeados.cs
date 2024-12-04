using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Operations.Dto.Responses.Premios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerPremiosCanjeados
{
    public class ObtenerPremiosCanjeadosCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
        public string? Nombre { get; set; } = null;
        public float? PuntosNecesarios { get; set; } = null;
        public TipoRubro? Rubro { get; set; }
    }

    internal class ObtenerPremiosCanjeadosHandler : IRequestHandler<ObtenerPremiosCanjeadosCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ObtenerPremiosCanjeadosHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ObtenerPremiosCanjeadosHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerPremiosCanjeadosCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener premios canjeados para el colaborador {ColaboradorId}",
                request.ColaboradorId);
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);

            if (colaborador == null)
            {
                return Results.BadRequest("El colaborador no existe");
            }

            var query = _unitOfWork.PremioRepository.GetQueryable();
            query = query
                .Where(p => p.ReclamadoPor != null && p.ReclamadoPor.Id == request.ColaboradorId);
            
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
            
            var premiosCanjeados = await _unitOfWork.PremioRepository.GetCollectionAsync(query);

            var response = premiosCanjeados.Select(p => _mapper.Map(p, p.GetType(), typeof(PremioResponse)));

            return Results.Ok(response);
        }
    }
}