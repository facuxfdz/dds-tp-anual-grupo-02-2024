using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class RegistrarVisitaHeladera
{
    public class RegistrarVisitaHeladeraCommand : IRequest<IResult>
    {
        public Guid IncidenteId { get; set; } = Guid.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Comentario { get; set; } = string.Empty;
        public bool Resuelto { get; set; } = false;
    }

    public class RegistrarVisitaHeladeraValidator : AbstractValidator<RegistrarVisitaHeladeraCommand>
    {
        public RegistrarVisitaHeladeraValidator()
        {
            RuleFor(x => x.IncidenteId)
                .NotNull();
            /*RuleFor(x => x.Foto)
                .NotNull();*/
            RuleFor(x => x.Fecha)
                .NotNull();
            /*RuleFor(x => x.Comentario)
                .NotNull();*/
        }
    }

    public class Handler : IRequestHandler<RegistrarVisitaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(RegistrarVisitaHeladeraCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegistrarVisitaHeladeraValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Results.Problem();
            }
            
            var incidente = await _unitOfWork.IncidenteRepository.GetByIdAsync(request.IncidenteId);
            if (incidente == null)
            {
                return Results.NotFound();
            }
            
            var visita = new VisitaTecnica
            {
                Foto = request.Foto,
                Fecha = request.Fecha,
                Comentario = request.Comentario
            };
            
            incidente.VisitasTecnicas.Add(visita);
            incidente.Resuelto = request.Resuelto;
            await _unitOfWork.VisitaTecnicaRepository.AddAsync(visita);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}