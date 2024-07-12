using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Email : MedioContacto
{
    public string Direccion { get; set; } = "";
    
    public Email()
    {
    }
    
    public Email(bool preferida, string direccion) : base(preferida)
    {
        Direccion = direccion;
    }

    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}