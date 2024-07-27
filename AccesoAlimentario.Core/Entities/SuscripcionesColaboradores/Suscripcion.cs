using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public abstract class Suscripcion : IObserverHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<Notificacion> Historial { get; private set; } = [];
    public Heladera Heladera { get; private set; } = null!;
    
    public Colaborador Colaborador { get; private set; } = null!;

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