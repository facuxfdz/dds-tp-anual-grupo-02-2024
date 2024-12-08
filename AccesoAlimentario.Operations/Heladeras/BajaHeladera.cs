using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Heladeras;

public static class BajaHeladera
{
    public class BajaHeladeraCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
    
    public class BajaHeladeraHandler : IRequestHandler<BajaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BajaHeladeraHandler> _logger;

        public BajaHeladeraHandler(IUnitOfWork unitOfWork, ILogger<BajaHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(BajaHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Baja heladera - {request.Id}");
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.Id}");
                return Results.NotFound("La heladera no existe");
            }

            foreach (var sensor in heladera.Sensores)
            {
                switch (sensor)
                {
                    case SensorTemperatura sensorTemperatura:
                        await _unitOfWork.RegistroTemperaturaRepository.RemoveRangeAsync(sensorTemperatura.RegistrosTemperatura);
                        await _unitOfWork.SensorTemperaturaRepository.RemoveAsync(sensorTemperatura);
                        break;
                    case SensorMovimiento sensorMovimiento:
                        await _unitOfWork.RegistroMovimientoRepository.RemoveRangeAsync(sensorMovimiento.RegistrosMovimiento);
                        await _unitOfWork.SensorMovimientoRepository.RemoveAsync(sensorMovimiento);
                        break;
                }
            }

            foreach (var suscripcion in heladera.Suscripciones)
            {
                await _unitOfWork.NotificacionRepository.RemoveRangeAsync(suscripcion.Historial);
            }
            
            await _unitOfWork.SuscripcionRepository.RemoveRangeAsync(heladera.Suscripciones);

            foreach (var incidente in heladera.Incidentes)
            {
                await _unitOfWork.VisitaTecnicaRepository.RemoveRangeAsync(incidente.VisitasTecnicas);
            }
            
            await _unitOfWork.IncidenteRepository.RemoveRangeAsync(heladera.Incidentes);

            foreach (var acceso in heladera.Accesos)
            {
                if (acceso.Autorizacion != null)
                {
                    await _unitOfWork.AutorizacionManipulacionHeladeraRepository.RemoveAsync(acceso.Autorizacion);
                }
            }
            
            await _unitOfWork.AccesoHeladeraRepository.RemoveRangeAsync(heladera.Accesos);

            var donacionViandasQuery = _unitOfWork.DonacionViandaRepository.GetQueryable()
                .Where(donacion => donacion.Heladera != null && donacion.Heladera.Id == heladera.Id);
            var donaciones = await _unitOfWork.DonacionViandaRepository.GetCollectionAsync(donacionViandasQuery);
            
            foreach (var donacion in donaciones)
            {
                donacion.Heladera = null;
                await _unitOfWork.DonacionViandaRepository.UpdateAsync(donacion);
            }
            
            var distribucionViandasQuery = _unitOfWork.DistribucionViandasRepository.GetQueryable()
                .Where(distribucion => distribucion.HeladeraOrigen != null && distribucion.HeladeraOrigen.Id == heladera.Id
                                       || distribucion.HeladeraDestino != null && distribucion.HeladeraDestino.Id == heladera.Id);
            var distribuciones = await _unitOfWork.DistribucionViandasRepository.GetCollectionAsync(distribucionViandasQuery);
            
            foreach (var distribucion in distribuciones)
            {
                if (distribucion.HeladeraOrigen != null && distribucion.HeladeraOrigen.Id == heladera.Id)
                {
                    distribucion.HeladeraOrigen = null;
                }
                if (distribucion.HeladeraDestino != null && distribucion.HeladeraDestino.Id == heladera.Id)
                {
                    distribucion.HeladeraDestino = null;
                }
                await _unitOfWork.DistribucionViandasRepository.UpdateAsync(distribucion);
            }

            foreach (var vianda in heladera.Viandas)
            {
                var queryV = _unitOfWork.AccesoHeladeraRepository.GetQueryable()
                    .Where(acceso => acceso.Viandas.Any(v => v.Id == vianda.Id));
                
                var accesos = await _unitOfWork.AccesoHeladeraRepository.GetCollectionAsync(queryV);
                
                await _unitOfWork.AccesoHeladeraRepository.RemoveRangeAsync(accesos);
            }
            
            await _unitOfWork.PuntoEstrategicoRepository.RemoveAsync(heladera.PuntoEstrategico);
            
            await _unitOfWork.ViandaRepository.RemoveRangeAsync(heladera.Viandas);
            
            await _unitOfWork.HeladeraRepository.RemoveAsync(heladera);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}