using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ConsultarEstadoHeladera
{
    public class ConsultarEstadoHeladeraCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class ConsultarEstadoHeladeraHandler : IRequestHandler<ConsultarEstadoHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConsultarEstadoHeladeraHandler> _logger;

        public ConsultarEstadoHeladeraHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ConsultarEstadoHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ConsultarEstadoHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Consultar estado de heladera - {request.Id}");
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.Id}");
                return Results.NotFound();
            }

            return Results.Ok(heladera.Estado);
        }
    }
}