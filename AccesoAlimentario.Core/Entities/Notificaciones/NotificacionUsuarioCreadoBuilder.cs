namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class NotificacionUsuarioCreadoBuilder : INotificacionBuilder
{
    private string _contrasenia;
    
    public NotificacionUsuarioCreadoBuilder(string contrasenia)
    {
        _contrasenia = contrasenia;
    }
    
    public Notificacion CrearNotificacion()
    {
        string asunto = "Acceso Alimentario: Su usuario ha sido creado con exito";
        string mensaje = $"Su usuario se ha creado con exito, su contraseña es: {_contrasenia}. No comparta esta información, muchas gracias.";
        return new Notificacion(asunto, mensaje);
    }
}