namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class Notificacion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Asunto { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public EstadoNotificacion Estado { get; set; } = EstadoNotificacion.Pendiente;
    
    public Notificacion()
    {
    }
    
    public Notificacion(string asunto, string mensaje)
    {
        Asunto = asunto;
        Mensaje = mensaje;
    }
}