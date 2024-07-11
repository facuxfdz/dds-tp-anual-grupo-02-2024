namespace AccesoAlimentario.Core.Entities.MediosContacto;

public abstract class MedioContacto
{
    protected List<Notificacion> _historial;
    protected bool _preferida;
    
    public MedioContacto(bool preferida)
    {
        _preferida = preferida;
        _historial = new List<Notificacion>();
    }
    
    public abstract void Enviar(Notificacion notificacion);
}