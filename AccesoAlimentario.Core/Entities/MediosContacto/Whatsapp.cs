using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Whatsapp : MedioContacto
{
    public string Numero { get; set; } = string.Empty;
    
    public Whatsapp()
    {
    }

    public Whatsapp(bool preferida, string numero) : base(preferida)
    {
        Numero = numero;
    }

    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}