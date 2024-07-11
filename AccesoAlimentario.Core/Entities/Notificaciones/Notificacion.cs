namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class Notificacion
{
    private string _asunto;
    private string _mensaje;
    private EstadoNotificacion _estado;
    
    public Notificacion(string asunto, string mensaje)
    {
        _asunto = asunto;
        _mensaje = mensaje;
        _estado = EstadoNotificacion.Pendiente;
    }
    
    public string Asunto => _asunto;
    public string Mensaje => _mensaje;
}