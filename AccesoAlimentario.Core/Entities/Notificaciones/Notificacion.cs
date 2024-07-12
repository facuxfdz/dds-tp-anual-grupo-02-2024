using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Notificaciones;

public class Notificacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Asunto { get; set; } = "";
    public string Mensaje { get; set; } = "";
    public EstadoNotificacion Estado { get; set; } = EstadoNotificacion.Pendiente;
    
    public Notificacion()
    {
    }
    
    public Notificacion(string asunto, string mensaje)
    {
        Asunto = asunto;
        Mensaje = mensaje;
    }
}