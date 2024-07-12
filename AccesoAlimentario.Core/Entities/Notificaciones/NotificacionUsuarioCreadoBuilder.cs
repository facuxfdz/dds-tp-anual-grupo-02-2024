namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class NotificacionUsuarioCreadoBuilder : INotificacionBuilder
{
    private string _username;
    private string _password;
    
    public NotificacionUsuarioCreadoBuilder(string username, string password)
    {
        _username = username;
        _password = password;
    }
    
    public Notificacion CrearNotificacion()
    {
        var asunto = "Acceso Alimentario: Su usuario ha sido creado con exito";
        var mensaje = $"Su usuario <b>{_username}</b> se ha creado con exito, su contraseña es: <b>{_password}</b>.<br>No comparta esta información, muchas gracias.";
        return new Notificacion(asunto, mensaje);
    }
}