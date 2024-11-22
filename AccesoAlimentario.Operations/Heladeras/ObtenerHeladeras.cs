using AccesoAlimentario.Core.DAL;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ObtenerHeladeras
{
    public class ObtenerHeladerasQuery : IRequest<IResult>
    {
    }
    
    public class ObtenerHeladerasHandler : IRequestHandler<ObtenerHeladerasQuery, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObtenerHeladerasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(ObtenerHeladerasQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.HeladeraRepository.GetQueryable();
            var heladeras = await _unitOfWork.HeladeraRepository.GetCollectionAsync(query);
            return Results.Ok(heladeras);
        }
    }
}