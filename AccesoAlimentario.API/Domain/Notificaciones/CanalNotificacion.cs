using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Notificaciones;

public abstract class CanalNotificacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public abstract void Notificar(string mensaje);
}