using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AutorizacionManipulacionHeladera
{
    private DateTime _fechaCreacion;
    public DateTime FechaExpiracion { get; private set; }
    public Heladera Heladera { get; private set; }
    private TarjetaColaboracion _tarjetaAutorizada;

    public AutorizacionManipulacionHeladera(DateTime fechaExpiracion, Heladera heladera,
        TarjetaColaboracion tarjetaAutorizada)
    {
        _fechaCreacion = DateTime.Now;
        FechaExpiracion = fechaExpiracion;
        Heladera = heladera;
        _tarjetaAutorizada = tarjetaAutorizada;
    }
}