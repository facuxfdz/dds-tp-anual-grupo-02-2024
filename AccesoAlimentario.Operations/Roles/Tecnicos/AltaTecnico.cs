using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Roles.Usuarios;
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
        private readonly ISender _sender;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper, ISender sender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sender = sender;
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

            var email = persona.MediosDeContacto.OfType<Email>().FirstOrDefault();
            if (email == null)
            {
                return Results.BadRequest("El técnico debe tener al menos un email de contacto");
            }

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


            var createUserCommand = new CrearUsuario.CrearUsuarioCommand
            {
                PersonaId = persona.Id,
                Username = email.Direccion,
                Password = CrearPassword(),
                ProfilePicture = "",
                RegisterType = RegisterType.Standard
            };

            return await _sender.Send(createUserCommand, cancellationToken);
        }

        private static string CrearPassword()
        {
            const int length = 16;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}