using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionIncidenteHeladera : Suscripcion
{
    public SuscripcionIncidenteHeladera()
    {
    }

    public override void CambioHeladera(Heladera heladera, CambioHeladeraTipo cambio)
    {
        if (cambio is not CambioHeladeraTipo.IncidenteProducido) return;
        var incidente = heladera.Incidentes.Last();
        switch (incidente)
        {
            case Alerta alerta:
                NotificarColaborador(new NotificacionIncidenteAlertaBuilder(alerta.Tipo).CrearNotificacion());
                break;
            case FallaTecnica ft:
                NotificarColaborador(new NotificacionIncidenteFallaTecnicaBuilder(ft.Descripcion, ft.Foto).CrearNotificacion());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cambio), cambio, null);
        }
    }
}