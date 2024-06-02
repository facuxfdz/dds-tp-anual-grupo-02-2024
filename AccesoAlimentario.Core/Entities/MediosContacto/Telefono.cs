namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Telefono : MedioContacto
{
    private string _numero;

    public Telefono(string numero)
    {
        _numero = numero;
    }
    
    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}