namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class NotificacionExcedenteBuilder : INotificacionBuilder
{
    public int CantidadHastaLimite { get; set; } = 0;

    public NotificacionExcedenteBuilder(int cantidadHastaLimite)
    {
        CantidadHastaLimite = cantidadHastaLimite;
    }

    public Notificacion CrearNotificacion()
    {
        var asunto = "Acceso Alimentario: Hay un exceso de viandas";
        var mensaje =
            $"Faltan {CantidadHastaLimite} viandas para que se llene la heladera. Por favor, distribuir en la brevedad.";
        return new Notificacion(asunto, mensaje);
    }
}