namespace AccesoAlimentario.Core.Entities.MediosContacto;

public abstract class MedioContacto
{
    protected List<Notificacion> _notificaciones;
    
    public MedioContacto()
    {
        _notificaciones = new List<Notificacion>();
    }
    
    public abstract void Enviar(Notificacion notificacion);
}