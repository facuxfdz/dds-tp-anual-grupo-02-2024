using AccesoAlimentario.Core.DAL;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ObtenerPuntajeColaborador
{
    public class ObtenerPuntajeColaboradorCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;
    }
    
    public class Handler : IRequestHandler<ObtenerPuntajeColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(ObtenerPuntajeColaboradorCommand request,
            CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(colaborador.Puntos);
        }
    }
}