using AccesoAlimentario.Core.DAL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class BajaTecnico
{
    public class BajaTecnicoCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class Handler : IRequestHandler<BajaTecnicoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(BajaTecnicoCommand request, CancellationToken cancellationToken)
        {
            var tecnico = await _unitOfWork.TecnicoRepository.GetByIdAsync(request.Id);
            if (tecnico == null)
            {
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