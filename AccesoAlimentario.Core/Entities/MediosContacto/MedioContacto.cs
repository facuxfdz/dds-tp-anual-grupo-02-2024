using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public abstract class MedioContacto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public virtual List<Notificacion> Historial { get; set; } = [];
    public bool Preferida { get; set; } = false;

    public MedioContacto()
    {
    }

    public MedioContacto(bool preferida)
    {
        Preferida = preferida;
    }

    public abstract void Enviar(Notificacion notificacion);
}