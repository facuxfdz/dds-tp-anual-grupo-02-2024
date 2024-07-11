using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities;

public abstract class Suscripcion
{
    private List<Notificacion> _historial;
    private Heladera _heladera;
    
    public Suscripcion(Heladera heladera)
    {
        _heladera = heladera;
        _historial = new List<Notificacion>();
    }

    public void notificarColaborador(Notificacion notificacion)
    {
        _historial.Add(notificacion); //TODO: es asi?
    }

    public void cambioHeladera(Heladera heladera)
    {
        _heladera = heladera; //TODO, algo mas?
    }

}