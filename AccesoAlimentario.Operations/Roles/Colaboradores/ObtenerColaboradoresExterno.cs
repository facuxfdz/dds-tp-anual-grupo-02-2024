using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerColaboradoresExterno
{
    public class ObtenerColaboradoresExternoCommand : IRequest<IResult>
    {
    }
    public class ObtenerColaboradoresExternoHandler : IRequestHandler<ObtenerColaboradoresExternoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public ObtenerColaboradoresExternoHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerColaboradoresExternoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(ObtenerColaboradoresExternoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Colaboradores Externo");
            var query = _unitOfWork.ColaboradorRepository.GetQueryable();
            var colaboradores = await _unitOfWork.ColaboradorRepository.GetCollectionAsync(query);
            var resColaboradores = colaboradores.Select(
                c => _mapper.Map(c, typeof(Colaborador), typeof(ColaboradorResponseExterno)));
            return Results.Ok(resColaboradores);
        }
    }
}