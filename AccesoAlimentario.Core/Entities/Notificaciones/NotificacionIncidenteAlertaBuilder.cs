using AccesoAlimentario.Core.Entities.Incidentes;

namespace AccesoAlimentario.Core.Entities.Notificaciones
{
    public class NotificacionIncidenteAlertaBuilder : INotificacionBuilder
    {
        private TipoAlerta _tipo;
        
        public override Notificacion CrearNotificacion()
        {
            string asunto = "Acceso Alimentario: Alerta de Sensor";
            string mensaje = $"Un sensor ha detectado una alerta de tipo {_tipo}. Por favor, acudir al lugar en la brevedad.";
            return new Notificacion(asunto, mensaje);
        }
    }
}