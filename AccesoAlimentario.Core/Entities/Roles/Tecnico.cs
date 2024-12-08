using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Tecnico : Rol
{
    public virtual AreaCobertura AreaCobertura { get; set; } = null!;
    public virtual List<VisitaTecnica> VisitasTecnicas { get; set; } = [];
    
    public Tecnico()
    {
    }
    
    public Tecnico(Persona persona, AreaCobertura areaCobertura) : base(persona)
    {
        AreaCobertura = areaCobertura;
    }

    public void ActualizarCobertura(AreaCobertura areaCobertura)
    {
        AreaCobertura.ActualizarArea(
            areaCobertura.Latitud,
            areaCobertura.Longitud,
            areaCobertura.Radio
        );
    }

    public bool ObtenerCercania(Heladera heladera)
    {
        var longitud = heladera.ObtenerLongitud();
        var latitud = heladera.ObtenerLatitud();
        return AreaCobertura.EsCercano(longitud, latitud);
    }
    
    public double ObtenerDistancia(Heladera heladera)
    {
        var longitud = heladera.ObtenerLongitud();
        var latitud = heladera.ObtenerLatitud();
        return AreaCobertura.ObtenerDistancia(longitud, latitud);
    }
    
    public void NotificarIncidente(Incidente incidente)
    {
        var notificacion = incidente switch
        {
            Alerta alerta => new NotificacionIncidenteAlertaBuilder(alerta.Tipo).CrearNotificacion(),
            FallaTecnica ft => new NotificacionIncidenteFallaTecnicaBuilder(ft.Descripcion, ft.Foto).CrearNotificacion(),
            _ => throw new ArgumentOutOfRangeException(nameof(incidente), incidente, null)
        };
        Persona.EnviarNotificacion(notificacion);
    }
}