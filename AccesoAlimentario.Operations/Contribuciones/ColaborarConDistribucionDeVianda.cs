using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDistribucionDeVianda
{
    public class ColaborarConDistribucionDeViandaCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.Now;
        public Guid HeladeraOrigenId { get; set; }
        public Guid HeladeraDestinoId { get; set; }
        public int CantidadDeViandas { get; set; }
        public MotivoDistribucion Motivo { get; set; }
    }
    
    public class Handler : IRequestHandler<ColaborarConDistribucionDeViandaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ColaborarConDistribucionDeViandaCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var heladeraOrigen = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraOrigenId);
            if (heladeraOrigen == null)
            {
                return Results.NotFound();
            }

            var heladeraDestino = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraDestinoId);
            if (heladeraDestino == null)
            {
                return Results.NotFound();
            }

            var viandas = heladeraOrigen.RetirarViandas(request.CantidadDeViandas);
            viandas.ForEach(vianda => heladeraDestino.IngresarVianda(vianda));
            var appSettings = AppSettings.Instance;
            colaborador.AgregarPuntos(appSettings.ViandasDistribuidasCoef * request.CantidadDeViandas);
            
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}