using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

[NotMapped]
public abstract class MedioContacto
{
    protected List<Notificacion> _historial;
    protected bool _preferida;
    
    public MedioContacto()
    {
    }
    
    public MedioContacto(bool preferida)
    {
        _preferida = preferida;
        _historial = new List<Notificacion>();
    }
    
    public abstract void Enviar(Notificacion notificacion);
}