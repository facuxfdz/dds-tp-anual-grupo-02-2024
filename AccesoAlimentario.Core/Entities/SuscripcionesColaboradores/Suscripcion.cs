using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.MediosContacto;

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
        // TODO
    }

    public void cambioHeladera(Heladera heladera)
    {
        // TODO 
    }

}