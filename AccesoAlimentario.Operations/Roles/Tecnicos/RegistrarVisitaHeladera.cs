using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Incidentes;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class RegistrarVisitaHeladera
{
    public class RegistrarVisitaHeladeraCommand : IRequest<IResult>
    {
        public Guid IncidenteId { get; set; } = Guid.Empty;
        public Guid TecnicoId { get; set; } = Guid.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
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

    public class RegistrarVisitaHeladeraHandler : IRequestHandler<RegistrarVisitaHeladeraCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegistrarVisitaHeladeraHandler> _logger;

        public RegistrarVisitaHeladeraHandler(IUnitOfWork unitOfWork, ILogger<RegistrarVisitaHeladeraHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(RegistrarVisitaHeladeraCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registrar Visita Heladera - {IncidenteId}", request.IncidenteId);
            var validator = new RegistrarVisitaHeladeraValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Datos de visita incorrectos - {IncidenteId}", request.IncidenteId);
                return Results.Problem();
            }
            
            var incidente = await _unitOfWork.IncidenteRepository.GetByIdAsync(request.IncidenteId);
            if (incidente == null)
            {
                _logger.LogWarning("Incidente no encontrado - {IncidenteId}", request.IncidenteId);
                return Results.NotFound();
            }
            
            var tecnico = await _unitOfWork.TecnicoRepository.GetByIdAsync(request.TecnicoId);
            if (tecnico == null)
            {
                _logger.LogWarning("Tecnico no encontrado - {TecnicoId}", request.TecnicoId);
                return Results.NotFound();
            }
            
            var visita = new VisitaTecnica
            {
                Foto = request.Foto,
                Fecha = request.Fecha,
                Comentario = request.Comentario,
                Tecnico = tecnico
            };
            
            incidente.VisitasTecnicas.Add(visita);
            incidente.Resuelto = request.Resuelto;
            await _unitOfWork.VisitaTecnicaRepository.AddAsync(visita);
            await _unitOfWork.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}