namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Email : MedioContacto
{
    private string _direccion;

    public Email(string direccion, bool preferida) : base(preferida)
    {
        _direccion = direccion;
    }

    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}