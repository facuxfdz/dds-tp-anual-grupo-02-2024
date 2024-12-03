using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionDeVianda
{
    public class ColaborarConDonacionDeViandaCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.Now;
        public Guid HeladeraId { get; set; }
        public string Comida { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public float Calorias { get; set; }
        public float Peso { get; set; }
        public EstadoVianda EstadoVianda { get; set; } = EstadoVianda.Disponible;
    }
    
    public class Handler : IRequestHandler<ColaborarConDonacionDeViandaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ColaborarConDonacionDeViandaCommand request, CancellationToken cancellationToken)
        {
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