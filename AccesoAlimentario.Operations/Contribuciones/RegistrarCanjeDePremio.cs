using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class RegistrarCanjeDePremio
{
    public class RegistrarCanjeDePremioCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public Guid PremioId { get; set; }
    }
    
    public class Handler : IRequestHandler<RegistrarCanjeDePremioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(RegistrarCanjeDePremioCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var premio = await _unitOfWork.PremioRepository.GetByIdAsync(request.PremioId);
            if (premio == null)
            {
                return Results.NotFound();
            }
            
            if (colaborador.Puntos < premio.PuntosNecesarios)
            {
                return Results.BadRequest("No tiene suficientes puntos para canjear el premio");
            }

            premio.ReclamadoPor = colaborador;
            premio.FechaReclamo = DateTime.Now;
            colaborador.Puntos -= premio.PuntosNecesarios;
            
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}