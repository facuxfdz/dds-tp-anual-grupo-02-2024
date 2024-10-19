using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Heladeras;

public static class ConsultarEstadoHeladera
{
    public class ConsultarEstadoHeladeraCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class Handler : IRequestHandler<ConsultarEstadoHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ConsultarEstadoHeladeraCommand request, CancellationToken cancellationToken)
        {
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.Id);
            if (heladera == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(heladera.Estado);
        }
    }
}