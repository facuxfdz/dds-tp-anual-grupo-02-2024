using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Operations.Heladeras;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Roles.Colaboradores;

public static class BajaColaborador
{
    public class BajaColaboradorCommand : IRequest<IResult>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class BajaColaboradorHandler : IRequestHandler<BajaColaboradorCommand, IResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _sender;
        private readonly ILogger _logger;

        public BajaColaboradorHandler(IUnitOfWork unitOfWork, ISender sender, ILogger<BajaColaboradorHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _sender = sender;
            _logger = logger;
        }

        public async Task<IResult> Handle(BajaColaboradorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Baja Colaborador");
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);
            if (colaborador == null)
            {
                _logger.LogWarning("Colaborador no encontrado - {Id}", request.Id);
                return Results.NotFound("Colaborador no encontrado");
            }

            var ofertaPremios = colaborador.ContribucionesRealizadas.OfType<OfertaPremio>();

            foreach (var ofertaPremio in ofertaPremios)
            {
                await _unitOfWork.PremioRepository.RemoveAsync(ofertaPremio.Premio);
            }

            var registrosPersonaVulnerable = colaborador.ContribucionesRealizadas.OfType<RegistroPersonaVulnerable>();

            foreach (var registroPersonaVulnerable in registrosPersonaVulnerable)
            {
                if (registroPersonaVulnerable.Tarjeta != null)
                {
                    registroPersonaVulnerable.Tarjeta.Responsable = null;
                    await _unitOfWork.TarjetaConsumoRepository.UpdateAsync(registroPersonaVulnerable.Tarjeta);
                }
            }
            
            var altaHeladeras = (colaborador.ContribucionesRealizadas.OfType<AdministracionHeladera>()).ToList();

            foreach (var heladera in altaHeladeras)
            {
                var command = new BajaHeladera.BajaHeladeraCommand { Id = heladera.Heladera.Id };
                await _sender.Send(command, cancellationToken);
            }
            
            var donacionViandas = colaborador.ContribucionesRealizadas.OfType<DonacionVianda>();
            
            foreach (var donacionVianda in donacionViandas)
            {
                if (donacionVianda.Vianda != null)
                {
                    donacionVianda.Vianda.Colaborador = null;
                    await _unitOfWork.ViandaRepository.UpdateAsync(donacionVianda.Vianda);
                }
            }

            await _unitOfWork.FormaContribucionRepository.RemoveRangeAsync(colaborador.ContribucionesRealizadas);

            foreach (var premio in colaborador.PremiosReclamados)
            {
                premio.ReclamadoPor = null;
                await _unitOfWork.PremioRepository.UpdateAsync(premio);
            }

            if (colaborador.TarjetaColaboracion != null)
            {
                foreach (var autorizacion in colaborador.TarjetaColaboracion.Autorizaciones)
                {
                    await _unitOfWork.AutorizacionManipulacionHeladeraRepository.RemoveAsync(autorizacion);
                }

                foreach (var acceso in colaborador.TarjetaColaboracion.Accesos)
                {
                    await _unitOfWork.AccesoHeladeraRepository.RemoveAsync(acceso);
                }

                await _unitOfWork.TarjetaColaboracionRepository.RemoveAsync(colaborador.TarjetaColaboracion);
            }

            var incidentesReportadosQuery = _unitOfWork.IncidenteRepository.GetQueryable();
            var incidentes = (await _unitOfWork.IncidenteRepository.GetCollectionAsync(incidentesReportadosQuery)).ToList();
            var fallas = incidentes.OfType<FallaTecnica>().Where(f => f.Colaborador?.Id == colaborador.Id);
            foreach (var falla in fallas)
            {
                falla.Colaborador = null;
                await _unitOfWork.IncidenteRepository.UpdateAsync(falla);
            }

            if (colaborador.Persona.Roles.Count == 2) // Solo tiene el rol de colaborador y usuario
            {
                if (colaborador.Persona.Direccion != null)
                {
                    await _unitOfWork.DireccionRepository.RemoveAsync(colaborador.Persona.Direccion);
                }
                await _unitOfWork.MedioContactoRepository.RemoveRangeAsync(colaborador.Persona.MediosDeContacto);
                if (colaborador.Persona.DocumentoIdentidad != null)
                {
                    await _unitOfWork.DocumentoIdentidadRepository.RemoveAsync(colaborador.Persona.DocumentoIdentidad);
                }
                await _unitOfWork.ColaboradorRepository.RemoveAsync(colaborador);
                await _unitOfWork.PersonaRepository.RemoveAsync(colaborador.Persona);
            }
            else
            {
                colaborador.Persona.Roles.Remove(colaborador);
                await _unitOfWork.PersonaRepository.UpdateAsync(colaborador.Persona);
                await _unitOfWork.ColaboradorRepository.RemoveAsync(colaborador);
            }

            await _unitOfWork.SaveChangesAsync();

            return Results.Ok();
        }
    }
}