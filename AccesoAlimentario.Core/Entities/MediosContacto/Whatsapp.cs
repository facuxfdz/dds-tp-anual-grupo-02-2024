namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Whatsapp : MedioContacto
{
    private string _numero;
    
    public Whatsapp(string numero)
    {
        _numero = numero;
    }

    public override void Contactar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}