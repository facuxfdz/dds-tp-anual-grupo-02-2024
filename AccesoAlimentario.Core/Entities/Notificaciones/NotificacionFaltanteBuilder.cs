namespace AccesoAlimentario.Core.Entities.Notificaciones
{
    public class NotificacionFaltanteBuilder : INotificacionBuilder
    {
        private int _cantidadHastaFaltante;
        
        public override Notificacion CrearNotificacion()
        {
            string asunto = "Acceso Alimentario: Hay un faltante viandas";
            string mensaje = $"Faltan {_cantidadHastaFaltante} viandas para vaciar la heladera. Por favor, reponer en la brevedad.";
            return new Notificacion(asunto, mensaje);
        }
    }
}