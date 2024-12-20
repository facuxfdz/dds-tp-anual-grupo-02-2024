﻿using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.MediosDeComunicacion;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Contribuciones;

public static class ColaborarConRegistroPersonaVulnerable
{
    public class ColaborarConRegistroPersonaVulnerableCommand : IRequest<IResult>
    {
        public Guid ColaboradorId { get; set; } = Guid.Empty;

        public TarjetaConsumoRequest Tarjeta { get; set; } = null!;

        // Para conformar la persona
        public PersonaHumanaRequest Persona { get; set; } = null!;
        public DireccionRequest? Direccion = null!;
        public DocumentoIdentidadRequest? Documento = null!;
        public List<MedioDeContactoRequest> MediosDeContacto = [];
        public int CantidadDeMenores { get; set; } = 0;
    }

    public class ColaborarConRegistroPersonaVulnerableHandler : IRequestHandler<ColaborarConRegistroPersonaVulnerableCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ColaborarConRegistroPersonaVulnerableHandler> _logger;

        public ColaborarConRegistroPersonaVulnerableHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<ColaborarConRegistroPersonaVulnerableHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(ColaborarConRegistroPersonaVulnerableCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"Colaborar con registro de persona vulnerable - {request.ColaboradorId} - {request.Tarjeta.Codigo}");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.ColaboradorId);
            if (colaborador == null)
            {
                _logger.LogWarning($"Colaborador no encontrado - {request.ColaboradorId}");
                return Results.NotFound();
            }

            var persona = _mapper.Map<Persona>(request.Persona);
            if (request.Direccion != null)
            {
                var direccion = _mapper.Map<Direccion>(request.Direccion);
                persona.Direccion = direccion;
            }

            if (request.Documento != null)
            {
                persona.DocumentoIdentidad = _mapper.Map<DocumentoIdentidad>(request.Documento);
            }

            persona.MediosDeContacto = _mapper.Map<List<MedioContacto>>(request.MediosDeContacto);


            var personaVulnerable = new PersonaVulnerable
            {
                Persona = persona,
                FechaRegistroSistema = DateTime.UtcNow,
                CantidadDeMenores = request.CantidadDeMenores,
            };

            var tarjetaConsumo = new TarjetaConsumo
            {
                Propietario = personaVulnerable,
                Codigo = request.Tarjeta.Codigo,
                Accesos = [],
                Responsable = colaborador,
            };

            personaVulnerable.Tarjeta = tarjetaConsumo;

            var colaboracion = new RegistroPersonaVulnerable
            {
                FechaContribucion = DateTime.UtcNow,
                Tarjeta = tarjetaConsumo,
            };

            colaborador.AgregarContribucion(colaboracion);
            var appSettings = AppSettings.Instance;
            colaborador.AgregarPuntos(appSettings.TarjetasRepartidasCoef * 1);

            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.PersonaVulnerableRepository.AddAsync(personaVulnerable);
            await _unitOfWork.TarjetaConsumoRepository.AddAsync(tarjetaConsumo);
            await _unitOfWork.RegistroPersonaVulnerableRepository.AddAsync(colaboracion);
            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}