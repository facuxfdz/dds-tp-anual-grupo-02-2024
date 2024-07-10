using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AutorizacionManipulacionHeladera
{
    private DateTime _fechaCreacion;
    private DateTime _fechaExpiracion;
    private Heladera _heladera;
    private TarjetaColaboracion _tarjetaAutorizada;
    
    public AutorizacionManipulacionHeladera(DateTime fechaExpiracion, Heladera heladera, TarjetaColaboracion tarjetaAutorizada)
    {
        _fechaCreacion = DateTime.Now;
        _fechaExpiracion = fechaExpiracion;
        _heladera = heladera;
        _tarjetaAutorizada = tarjetaAutorizada;
    }
}