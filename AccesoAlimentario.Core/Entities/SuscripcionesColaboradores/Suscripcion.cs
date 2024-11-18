using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public abstract class Suscripcion : IObserverHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public virtual List<Notificacion> Historial { get; set; } = [];
    public virtual Heladera Heladera { get; set; } = null!;
    
    public virtual Colaborador Colaborador { get; set; } = null!;

    public Suscripcion()
    {
    }

    public Suscripcion(Heladera heladera, Colaborador colaborador)
    {
        Heladera = heladera;
        Colaborador = colaborador;
        heladera.Suscribirse(this);
    }

    public void NotificarColaborador(Notificacion notificacion)
    {
        Historial.Add(notificacion);
        Colaborador.Persona.EnviarNotificacion(notificacion);
    }

    public abstract void CambioHeladera(Heladera heladera, CambioHeladeraTipo cambio);
}