using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public abstract class Suscripcion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<Notificacion> Historial { get; private set; } = [];
    public Heladera Heladera { get; private set; } = null!;

    public Suscripcion()
    {
    }

    public Suscripcion(Heladera heladera)
    {
        Heladera = heladera;
    }

    public void NotificarColaborador(Notificacion notificacion)
    {
        Historial.Add(notificacion); //TODO: es asi?
    }

    public void CambioHeladera(Heladera heladera)
    {
        Heladera = heladera; //TODO, algo mas?
    }
}