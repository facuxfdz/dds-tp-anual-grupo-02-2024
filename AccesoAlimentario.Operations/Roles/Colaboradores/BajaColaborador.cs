using AccesoAlimentario.Core.DAL;
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
            
            await _unitOfWork.ColaboradorRepository.RemoveAsync(colaborador);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}