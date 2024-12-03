using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ConsultarHeladera
{
    public class ConsultarHeladeraQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }

    public class ConsultarHeladeraHandler : IRequestHandler<ConsultarHeladeraQuery, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ConsultarHeladeraHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ConsultarHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ConsultarHeladeraQuery request, CancellationToken cancellationToken)
        {
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);

            if (heladera == null)
            {
                _logger.LogWarning("Heladera no encontrada");
                return Results.NotFound();
            }

            return Results.Ok(_mapper.Map(heladera, typeof(Heladera), typeof(HeladeraResponse)));
        }
    }
}