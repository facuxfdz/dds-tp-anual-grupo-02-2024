using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConDonacionMonetaria
{
    public class ColaborarConDonacionMonetariaCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.Now;
        public float Monto { get; set; }
        public int FrecuenciaDias { get; set; }
    }
    
    public class Handler : IRequestHandler<ColaborarConDonacionMonetariaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ColaborarConDonacionMonetariaCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var donacion = new DonacionMonetaria
            {
                FechaContribucion = request.FechaContribucion,
                Monto = request.Monto,
                FrecuenciaDias = request.FrecuenciaDias
            };
            
            colaborador.AgregarContribucion(donacion);
            var appSettings = AppSettings.Instance;
            colaborador.AgregarPuntos(appSettings.PesoDonadosCoef * request.Monto);
            
            await _unitOfWork.DonacionMonetariaRepository.AddAsync(donacion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}