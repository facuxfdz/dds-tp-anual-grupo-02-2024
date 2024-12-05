using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class BajaColaborador
{
    public class BajaColaboradorCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<BajaColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IResult> Handle(BajaColaboradorCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);
            if (colaborador == null)
            {
                return Results.NotFound("El colaborador no existe");
            }

            var ofertaPremios = colaborador.ContribucionesRealizadas.OfType<OfertaPremio>();
            
            foreach (var ofertaPremio in ofertaPremios)
            {
                await _unitOfWork.PremioRepository.RemoveAsync(ofertaPremio.Premio);
            }
            
            var registrosPersonaVulnerable = colaborador.ContribucionesRealizadas.OfType<RegistroPersonaVulnerable>();
            
            foreach (var registroPersonaVulnerable in registrosPersonaVulnerable)
            {
                if (registroPersonaVulnerable.Tarjeta != null)
                {
                    registroPersonaVulnerable.Tarjeta.Responsable = null;
                    await _unitOfWork.TarjetaConsumoRepository.UpdateAsync(registroPersonaVulnerable.Tarjeta);
                }
            }
            
            await _unitOfWork.FormaContribucionRepository.RemoveRangeAsync(colaborador.ContribucionesRealizadas);

            foreach (var premio in colaborador.PremiosReclamados)
            {
                premio.ReclamadoPor = null;
                await _unitOfWork.PremioRepository.UpdateAsync(premio);
            }
            
            await _unitOfWork.ColaboradorRepository.RemoveAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}