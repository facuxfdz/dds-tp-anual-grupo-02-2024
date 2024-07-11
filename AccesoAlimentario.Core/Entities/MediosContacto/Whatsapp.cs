using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Whatsapp : MedioContacto
{
    private string _numero;
    
    public Whatsapp(string numero, bool preferida) : base(preferida)
    {
        _numero = numero;
    }

    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}