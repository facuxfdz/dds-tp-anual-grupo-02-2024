namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Whatsapp : MedioContacto
{
    private string _numero;
    
    public Whatsapp(string numero)
    {
        _numero = numero;
    }

    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}