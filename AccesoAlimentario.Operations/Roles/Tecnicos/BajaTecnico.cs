using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class BajaTecnico
{
    public class BajaTecnicoCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class BajaTecnicoHandler : IRequestHandler<BajaTecnicoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BajaTecnicoHandler> _logger;

        public BajaTecnicoHandler(IUnitOfWork unitOfWork, ILogger<BajaTecnicoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Handle(BajaTecnicoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Baja Tecnico - {Id}", request.Id);
            var tecnico = await _unitOfWork.TecnicoRepository.GetByIdAsync(request.Id);
            if (tecnico == null)
            {
                _logger.LogWarning("Tecnico no encontrado - {Id}", request.Id);
                return Results.NotFound("El tecnico no existe");
            }
            
            await _unitOfWork.AreaCoberturaRepository.RemoveAsync(tecnico.AreaCobertura);

            foreach (var visita in tecnico.VisitasTecnicas)
            {
                visita.Tecnico = null;
                await _unitOfWork.VisitaTecnicaRepository.UpdateAsync(visita);
            }

            try
            {
                if (tecnico.Persona.Roles.Count == 2)
                {
                    if (tecnico.Persona.Direccion != null)
                    {
                        await _unitOfWork.DireccionRepository.RemoveAsync(tecnico.Persona.Direccion);
                    }

                    await _unitOfWork.MedioContactoRepository.RemoveRangeAsync(tecnico.Persona.MediosDeContacto);
                    if (tecnico.Persona.DocumentoIdentidad != null)
                    {
                        await _unitOfWork.DocumentoIdentidadRepository.RemoveAsync(tecnico.Persona.DocumentoIdentidad);
                    }

                    await _unitOfWork.PersonaRepository.RemoveAsync(tecnico.Persona);
                }
                else
                {
                    tecnico.Persona.Roles.Remove(tecnico);
                    await _unitOfWork.PersonaRepository.UpdateAsync(tecnico.Persona);
                }
            }
            catch
            {
                // ignored
            }


            await _unitOfWork.TecnicoRepository.RemoveAsync(tecnico);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}