namespace AccesoAlimentario.Core.Entities.Notificaciones
{
    public class NotificacionExcedenteBuilder : INotificacionBuilder
    {
        private int _cantidadHastaLimite;
        
        public NotificacionExcedenteBuilder(int cantidadHastaLimite)
        {
            _cantidadHastaLimite = cantidadHastaLimite;
        }
        
        public Notificacion CrearNotificacion()
        {
            string asunto = "Acceso Alimentario: Hay un exceso de viandas";
            string mensaje = $"Faltan {_cantidadHastaLimite} viandas para que se llene la heladera. Por favor, distribuir en la brevedad.";
            return new Notificacion(asunto, mensaje);
        }
    }
}