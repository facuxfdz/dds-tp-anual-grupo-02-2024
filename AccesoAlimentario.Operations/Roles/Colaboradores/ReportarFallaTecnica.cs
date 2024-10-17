using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class ReportarFallaTecnica
{
    public class ReportarFallaTecnicaCommand : IRequest<IResult>
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public Guid ReporteroId { get; set; } = Guid.Empty;
        public Guid HeladeraId { get; set; } = Guid.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
    }

    public class AltaColaboradorValidator : AbstractValidator<ReportarFallaTecnicaCommand>
    {
        public AltaColaboradorValidator()
        {
            RuleFor(x => x.Fecha)
                .NotNull();
            RuleFor(x => x.ReporteroId)
                .NotNull();
            /*RuleFor(x => x.Descripcion)
                .NotNull();*/
            /*RuleFor(x => x.Foto)
                .NotNull();*/
        }
    }

    public class Handler : IRequestHandler<ReportarFallaTecnicaCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ReportarFallaTecnicaCommand request, CancellationToken cancellationToken)
        {
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ReporteroId);
            
            if (colaborador == null)
            {
                return Results.NotFound();
            }
            
            var heladera = await _unitOfWork.HeladeraRepository.GetByIdAsync(request.HeladeraId);
            
            if (heladera == null)
            {
                return Results.NotFound();
            }
            
            var fallaTecnica = new FallaTecnica
            {
                Fecha = request.Fecha,
                Colaborador = colaborador,
                Descripcion = request.Descripcion,
                Foto = request.Foto
            };
            
            heladera.AgregarIncidente(fallaTecnica);

            await _unitOfWork.IncidenteRepository.AddAsync(fallaTecnica);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}