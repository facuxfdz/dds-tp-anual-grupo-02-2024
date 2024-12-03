using AccesoAlimentario.Core.DAL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class DesuscribirseHeladera
{
    public class DesuscribirseHeladeraCommand : IRequest<IResult>
    {
        public Guid SuscripcionId { get; set; } = Guid.Empty;
    }
    
    internal class DesuscribirseHeladeraHandler : IRequestHandler<DesuscribirseHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DesuscribirseHeladeraHandler(IUnitOfWork unitOfWork, ILogger<DesuscribirseHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(DesuscribirseHeladeraCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Desuscribiendo heladera");
            var suscripcion = await _unitOfWork.SuscripcionRepository.GetByIdAsync(request.SuscripcionId);
            if (suscripcion == null)
            {
                return Results.NotFound();
            }

            await _unitOfWork.SuscripcionRepository.RemoveAsync(suscripcion);
            await _unitOfWork.SaveChangesAsync();
            return Results.Ok();
        }
    }
}