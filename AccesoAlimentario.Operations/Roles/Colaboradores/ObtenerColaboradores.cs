using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Dto.Responses.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerColaboradores
{
    public class ObtenerColaboradoresCommand : IRequest<IResult>
    {
    }

    internal class ObtenerColaboradoresHandler : IRequestHandler<ObtenerColaboradoresCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ObtenerColaboradoresHandler> _logger;

        public ObtenerColaboradoresHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ObtenerColaboradoresHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerColaboradoresCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Colaboradores");
            var query = _unitOfWork.ColaboradorRepository.GetQueryable();
            var colaboradores = await _unitOfWork.ColaboradorRepository.GetCollectionAsync(query);

            var response = colaboradores.Select(c => _mapper.Map<ColaboradorResponse>(c));
            return Results.Ok(response);
        }
    }
}