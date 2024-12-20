﻿using System.Text.Json;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;
using AccesoAlimentario.Operations.Dto.Responses.Incidentes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ObtenerHeladeras
{
    public class ObtenerHeladerasQuery : IRequest<IResult>
    {
    }
    
    public class ObtenerHeladerasHandler : IRequestHandler<ObtenerHeladerasQuery, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ObtenerHeladerasHandler(IUnitOfWork unitOfWork, ILogger<ObtenerHeladerasHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ObtenerHeladerasQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtener Heladeras");
            var query = _unitOfWork.HeladeraRepository.GetQueryable();
            var heladeras = (await _unitOfWork.HeladeraRepository.GetCollectionAsync(query)).ToList();
            var response = heladeras.Select<Heladera, HeladeraResponse>(h => (HeladeraResponse)_mapper.Map(h, h.GetType(), typeof(HeladeraResponse)));
            
            return Results.Ok(response);
        }
    }
}