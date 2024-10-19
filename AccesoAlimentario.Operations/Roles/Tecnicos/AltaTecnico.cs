using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class AltaTecnico
{
    public class AltaTecnicoCommand : IRequest<IResult>
    {
        public PersonaRequest Persona { get; set; } = null!;
        public DocumentoIdentidadRequest Documento = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        public float AreaCoberturaLatitud { get; set; } = 0;
        public float AreaCoberturaLongitud { get; set; } = 0;
        public float AreaCoberturaRadio { get; set; } = 0;
    }

    // Validaciones
    public class AltaTecnicoValidator : AbstractValidator<AltaTecnicoCommand>
    {
        public AltaTecnicoValidator()
        {
            RuleFor(x => x.Persona)
                .NotNull();
            RuleFor(x => x.Documento)
                .NotNull();
            RuleFor(x => x.MediosDeContacto)
                .NotNull();
            RuleFor(x => x.AreaCoberturaLatitud)
                .NotNull();
            RuleFor(x => x.AreaCoberturaLongitud)
                .NotNull();
            RuleFor(x => x.AreaCoberturaRadio)
                .NotNull();
        }
    }

    // Handler
    public class Handler : IRequestHandler<AltaTecnicoCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(AltaTecnicoCommand request, CancellationToken cancellationToken)
        {
            var validator = new AltaTecnicoValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Results.Problem();
            }

            var persona = _mapper.Map<Persona>(request.Persona);
            persona.DocumentoIdentidad = _mapper.Map<DocumentoIdentidad>(request.Documento);
            persona.MediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);

            var tecnico = new Tecnico
            {
                Persona = persona,
                AreaCobertura = new AreaCobertura
                {
                    Latitud = request.AreaCoberturaLatitud,
                    Longitud = request.AreaCoberturaLongitud,
                    Radio = request.AreaCoberturaRadio
                }
            };

            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.TecnicoRepository.AddAsync(tecnico);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok(tecnico);
        }
    }
}