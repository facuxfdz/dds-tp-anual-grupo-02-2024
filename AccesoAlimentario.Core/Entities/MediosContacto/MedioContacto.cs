using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public abstract class MedioContacto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<Notificacion> Historial { get; set; } = [];
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