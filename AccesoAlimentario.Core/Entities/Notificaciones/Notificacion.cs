namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class Notificacion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Asunto { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    
    public virtual List<ImagenCid> Imagenes { get; set; } = [];
    
    public EstadoNotificacion Estado { get; set; } = EstadoNotificacion.Pendiente;
    
    public Notificacion()
    {
    }
    
    public Notificacion(string asunto, string mensaje)
    {
        Asunto = asunto;
        Mensaje = mensaje;
    }
    
    public Notificacion(string asunto, string mensaje, List<ImagenCid> imagenes)
    {
        Asunto = asunto;
        Mensaje = mensaje;
        Imagenes = imagenes;
    }
}

public class ImagenCid
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Cid { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
}