using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Premios;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConOfertaDePremio
{
    public class ColaborarConOfertaDePremioCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; }
        public DateTime FechaContribucion { get; set; } = DateTime.UtcNow;
        public string Nombre { get; set; }
        public float PuntosNecesarios { get; set; }
        public string Imagen { get; set; }
        public TipoRubro Rubro { get; set; }
    }
    
    public class Handler : IRequestHandler<ColaborarConOfertaDePremioCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ColaborarConOfertaDePremioCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            var premio = new Premio
            {
                Nombre = request.Nombre,
                PuntosNecesarios = request.PuntosNecesarios,
                Imagen = request.Imagen,
                Rubro = request.Rubro
            };

            var contribucion = new OfertaPremio
            {
                FechaContribucion = request.FechaContribucion,
                Premio = premio
            };
            
            colaborador.AgregarContribucion(contribucion);
            
            await _unitOfWork.PremioRepository.AddAsync(premio);
            await _unitOfWork.OfertaPremioRepository.AddAsync(contribucion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}