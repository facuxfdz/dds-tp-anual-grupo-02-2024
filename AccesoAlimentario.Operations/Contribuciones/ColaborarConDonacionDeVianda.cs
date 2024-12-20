﻿using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionDeVianda
{
    public class ColaborarConDonacionDeViandaCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
        public Guid HeladeraId { get; set; }
        public string Comida { get; set; } = string.Empty;
        public DateTime FechaCaducidad { get; set; }
        public float Calorias { get; set; }
        public float Peso { get; set; }
        public EstadoVianda EstadoVianda { get; set; } = EstadoVianda.Disponible;
    }
    
    public class ColaborarConDonacionDeViandaHandler : IRequestHandler<ColaborarConDonacionDeViandaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ColaborarConDonacionDeViandaHandler> _logger;

        public ColaborarConDonacionDeViandaHandler(IUnitOfWork unitOfWork, ILogger<ColaborarConDonacionDeViandaHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(ColaborarConDonacionDeViandaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Colaborar con donación de vianda - {request.ColaboradorId} - {request.FechaContribucion}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            if (heladera == null)
            {
                _logger.LogWarning($"Heladera no encontrada - {request.HeladeraId}");
                return Results.NotFound();
            }

            var unidadEstandar = await _unitOfWork.ViandaEstandarRepository.GetAsync(
                _unitOfWork.ViandaEstandarRepository.GetQueryable()
                );
            
            if (unidadEstandar == null)
            {
                unidadEstandar = new ViandaEstandar
                {
                    Largo = 10,
                    Ancho = 10,
                    Profundidad = 10
                };
                await _unitOfWork.ViandaEstandarRepository.AddAsync(unidadEstandar);
                await _unitOfWork.SaveChangesAsync();
            }

            var vianda = new Vianda
            {
                Comida = request.Comida,
                FechaDonacion = request.FechaContribucion,
                FechaCaducidad = request.FechaCaducidad,
                Colaborador = colaborador,
                Calorias = request.Calorias,
                Peso = request.Peso,
                Estado = request.EstadoVianda,
                UnidadEstandar = unidadEstandar
            };

            var colaboracion = new DonacionVianda
            {
                FechaContribucion = request.FechaContribucion,
                Heladera = heladera,
                Vianda = vianda
            };
            
            colaborador.AgregarContribucion(colaboracion);
            heladera.IngresarVianda(vianda);
            
            var appSettings = AppSettings.Instance;
            colaborador.AgregarPuntos(appSettings.PesoDonadosCoef * request.Peso);
            
            await _unitOfWork.ViandaRepository.AddAsync(vianda);
            await _unitOfWork.DonacionViandaRepository.AddAsync(colaboracion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}