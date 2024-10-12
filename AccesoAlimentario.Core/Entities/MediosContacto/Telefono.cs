using AccesoAlimentario.Core.Entities.Notificaciones;

namespace AccesoAlimentario.Core.Entities.MediosContacto;

public class Telefono : MedioContacto
{
    public string Numero { get; set; } = string.Empty;
    
    public Telefono()
    {
    }
    
    public Telefono(bool preferida, string numero) : base(preferida)
    {
        Numero = numero;
    }
    
    public override void Enviar(Notificacion notificacion)
    {
        throw new NotImplementedException();
    }
}