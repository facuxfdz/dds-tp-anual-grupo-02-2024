using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Operations.Dto.Responses.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class ObtenerTecnicos
{
    public class ObtenerTecnicosCommand : IRequest<IResult>
    {
    }
    
    internal class ObtenerTecnicosHandler : IRequestHandler<ObtenerTecnicosCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ObtenerTecnicosHandler> _logger;

        public ObtenerTecnicosHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ObtenerTecnicosHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ObtenerTecnicosCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Tecnicos");
            var query = _unitOfWork.TecnicoRepository.GetQueryable();
            var tecnicos = await _unitOfWork.TecnicoRepository.GetCollectionAsync(query);

            var response = tecnicos.Select(c => _mapper.Map(c, c.GetType(), typeof(TecnicoResponse)));
            return Results.Ok(response);
        }
    }
}