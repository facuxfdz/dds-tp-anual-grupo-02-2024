namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Notificacion
{
    private string _asunto;
    private string _mensaje;
    
    public Notificacion(string asunto, string mensaje)
    {
        _asunto = asunto;
        _mensaje = mensaje;
    }
    
    public string Asunto => _asunto;
    public string Mensaje => _mensaje;
}