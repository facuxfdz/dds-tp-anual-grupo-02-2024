using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Infraestructura.EmailObj;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Email : MedioContacto
{
    public string Direccion { get; set; } = string.Empty;

    public Email()
    {
    }

    public Email(bool preferida, string direccion) : base(preferida)
    {
        Direccion = direccion;
    }

    public override void Enviar(Notificacion notificacion)
    {
        var e = new EmailService();
        e.Enviar("Grupo 02", this.Direccion, notificacion.Asunto, notificacion.Mensaje);
        this.Historial.Add(notificacion);
    }
}