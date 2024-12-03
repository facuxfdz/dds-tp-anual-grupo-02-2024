using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class SuscribirseHeladera
{
    public class SuscribirseHeladeraCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
        
        public int Minimo { get; set; } = 0;
        public int Maximo { get; set; } = 0;
        public TipoSuscripcion Tipo { get; set; } = TipoSuscripcion.Faltante;
        
        public enum TipoSuscripcion
        {
            Faltante,
            Excedente,
            Incidente
        }
    }
    
    public class SuscribirseHeladeraHandler : IRequestHandler<SuscribirseHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public SuscribirseHeladeraHandler(IUnitOfWork unitOfWork, ILogger<SuscribirseHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(SuscribirseHeladeraCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Suscribirse a heladera"); 
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            if (heladera == null)
            {
                return Results.NotFound();
            }
            
            Suscripcion suscripcion;
            
            switch (request.Tipo)
            {
                case SuscribirseHeladeraCommand.TipoSuscripcion.Faltante:
                    suscripcion = new SuscripcionFaltanteViandas
                    {
                        Minimo = request.Minimo,
                        Heladera = heladera,
                        Colaborador = colaborador
                    };
                    break;
                case SuscribirseHeladeraCommand.TipoSuscripcion.Excedente:
                    suscripcion = new SuscripcionExcedenteViandas
                    {
                        Maximo = request.Maximo,
                        Heladera = heladera,
                        Colaborador = colaborador
                    };
                    break;
                case SuscribirseHeladeraCommand.TipoSuscripcion.Incidente:
                    suscripcion = new SuscripcionIncidenteHeladera
                    {
                        Heladera = heladera,
                        Colaborador = colaborador
                    };
                    break;
                default:
                    return Results.BadRequest();
            }

            await _unitOfWork.SuscripcionRepository.AddAsync(suscripcion);
            colaborador.AgregarSubscripcion(suscripcion);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}