using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Responses.Externos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public class ObtenerColaboradores
{
    public class ObtenerAllCommand : IRequest<IResult>
    {
    }
    public class ObtenerAllHandler : IRequestHandler<ObtenerAllCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public ObtenerAllHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ObtenerAllHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IResult> Handle(ObtenerAllCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo colaboradores");
            var query = _unitOfWork.ColaboradorRepository.GetQueryable();
            var colaboradores = await _unitOfWork.ColaboradorRepository.GetCollectionAsync(query);
            var resColaboradores = colaboradores.Select(
                c => _mapper.Map(c, typeof(Colaborador), typeof(ColaboradorResponseExterno)));
            return Results.Ok(resColaboradores);
        }
    }
}