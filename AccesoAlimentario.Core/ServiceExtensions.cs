using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Sensores;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Core;

public static class ServiceExtensions
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBaseRepository<Persona>, BaseRepository<Persona>>();
        services.AddScoped<IBaseRepository<PersonaHumana>, BaseRepository<PersonaHumana>>();
        services.AddScoped<IBaseRepository<PersonaJuridica>, BaseRepository<PersonaJuridica>>();
        services.AddScoped<IBaseRepository<MedioContacto>, BaseRepository<MedioContacto>>();
        services.AddScoped<IBaseRepository<Telefono>, BaseRepository<Telefono>>();
        services.AddScoped<IBaseRepository<Email>, BaseRepository<Email>>();
        services.AddScoped<IBaseRepository<Whatsapp>, BaseRepository<Whatsapp>>();
        services.AddScoped<IBaseRepository<DocumentoIdentidad>, BaseRepository<DocumentoIdentidad>>();
        services.AddScoped<IBaseRepository<Direccion>, BaseRepository<Direccion>>();
        services.AddScoped<IBaseRepository<Rol>, BaseRepository<Rol>>();
        services.AddScoped<IBaseRepository<UsuarioSistema>, BaseRepository<UsuarioSistema>>();
        services.AddScoped<IBaseRepository<PersonaVulnerable>, BaseRepository<PersonaVulnerable>>();
        services.AddScoped<IBaseRepository<Tecnico>, BaseRepository<Tecnico>>();
        services.AddScoped<IBaseRepository<Colaborador>, BaseRepository<Colaborador>>();
        services.AddScoped<IBaseRepository<AreaCobertura>, BaseRepository<AreaCobertura>>();
        services.AddScoped<IBaseRepository<Tarjeta>, BaseRepository<Tarjeta>>();
        services.AddScoped<IBaseRepository<TarjetaConsumo>, BaseRepository<TarjetaConsumo>>();
        services.AddScoped<IBaseRepository<TarjetaColaboracion>, BaseRepository<TarjetaColaboracion>>();
        services.AddScoped<IBaseRepository<FormaContribucion>, BaseRepository<FormaContribucion>>();
        services.AddScoped<IBaseRepository<AdministracionHeladera>, BaseRepository<AdministracionHeladera>>();
        services.AddScoped<IBaseRepository<DistribucionViandas>, BaseRepository<DistribucionViandas>>();
        services.AddScoped<IBaseRepository<RegistroPersonaVulnerable>, BaseRepository<RegistroPersonaVulnerable>>();
        services.AddScoped<IBaseRepository<DonacionMonetaria>, BaseRepository<DonacionMonetaria>>();
        services.AddScoped<IBaseRepository<DonacionVianda>, BaseRepository<DonacionVianda>>();
        services.AddScoped<IBaseRepository<OfertaPremio>, BaseRepository<OfertaPremio>>();
        services.AddScoped<IBaseRepository<Heladera>, BaseRepository<Heladera>>();
        services.AddScoped<IBaseRepository<Vianda>, BaseRepository<Vianda>>();
        services.AddScoped<IBaseRepository<ViandaEstandar>, BaseRepository<ViandaEstandar>>();
        services.AddScoped<IBaseRepository<PuntoEstrategico>, BaseRepository<PuntoEstrategico>>();
        services.AddScoped<IBaseRepository<ModeloHeladera>, BaseRepository<ModeloHeladera>>();
        services.AddScoped<IBaseRepository<Sensor>, BaseRepository<Sensor>>();
        services.AddScoped<IBaseRepository<SensorMovimiento>, BaseRepository<SensorMovimiento>>();
        services.AddScoped<IBaseRepository<SensorTemperatura>, BaseRepository<SensorTemperatura>>();
        services.AddScoped<IBaseRepository<RegistroMovimiento>, BaseRepository<RegistroMovimiento>>();
        services.AddScoped<IBaseRepository<RegistroTemperatura>, BaseRepository<RegistroTemperatura>>();
        services.AddScoped<IBaseRepository<Incidente>, BaseRepository<Incidente>>();
        services.AddScoped<IBaseRepository<VisitaTecnica>, BaseRepository<VisitaTecnica>>();
        services.AddScoped<IBaseRepository<Premio>, BaseRepository<Premio>>();
        services.AddScoped<IBaseRepository<Notificacion>, BaseRepository<Notificacion>>();
        services.AddScoped<IBaseRepository<Suscripcion>, BaseRepository<Suscripcion>>();
        services.AddScoped<IBaseRepository<SuscripcionFaltanteViandas>, BaseRepository<SuscripcionFaltanteViandas>>();
        services.AddScoped<IBaseRepository<SuscripcionExcedenteViandas>, BaseRepository<SuscripcionExcedenteViandas>>();
        services.AddScoped<IBaseRepository<SuscripcionIncidenteHeladera>, BaseRepository<SuscripcionIncidenteHeladera>>();
        services.AddScoped<IBaseRepository<AccesoHeladera>, BaseRepository<AccesoHeladera>>();
        services.AddScoped<IBaseRepository<AutorizacionManipulacionHeladera>, BaseRepository<AutorizacionManipulacionHeladera>>();
    }
}