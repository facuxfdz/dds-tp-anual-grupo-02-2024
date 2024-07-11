using AccesoAlimentario.Core.Entities.Incidente;

namespace AccesoAlimentario.Core.Entities.Notificaciones
{
    public class NotificacionIncidenteFallaTecnicaBuilder : INotificacionBuilder
    {
        private string? descripcion;
        private string? foto;
        
        public override Notificacion CrearNotificacion()
        {
            string asunto = "Acceso Alimentario: Un usuario ha reportado una Falla Técnica";
            string mensaje = $"Un usuario ha reportado una falla tecnica en una heladera. Por favor, acudir al lugar en la brevedad.";
            if (descripcion != null || foto != null)
            {
                mensaje += "\nSe adjunta informacion adicional proporcionada por el usuario";
            }
            if (descripcion != null)
            {
                mensaje += $"\nDescripción: {descripcion}";
            }
            if (foto != null)
            {
                mensaje += $"\nFoto: {foto}";
            }
            return new Notificacion(asunto, mensaje);
        }
    }
}