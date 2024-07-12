namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class NotificacionIncidenteFallaTecnicaBuilder : INotificacionBuilder
{
    private string? _descripcion;
    private string? _foto;
    
    public NotificacionIncidenteFallaTecnicaBuilder(string? descripcion, string? foto)
    {
        this._descripcion = descripcion;
        this._foto = foto;
    }
        
    public Notificacion CrearNotificacion()
    {
        var asunto = "Acceso Alimentario: Un usuario ha reportado una Falla Técnica";
        var mensaje = $"Un usuario ha reportado una falla tecnica en una heladera. Por favor, acudir al lugar en la brevedad.";
        if (_descripcion != null || _foto != null)
        {
            mensaje += "\nSe adjunta informacion adicional proporcionada por el usuario";
        }
        if (_descripcion != null)
        {
            mensaje += $"\nDescripción: {_descripcion}";
        }
        if (_foto != null)
        {
            mensaje += $"\nFoto: {_foto}";
        }
        return new Notificacion(asunto, mensaje);
    }
}