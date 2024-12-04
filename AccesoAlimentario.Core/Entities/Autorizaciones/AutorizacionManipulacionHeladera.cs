using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AutorizacionManipulacionHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaExpiracion { get; set; } = DateTime.UtcNow;
    public virtual Heladera Heladera { get; set; } = null!;
    public virtual TarjetaColaboracion TarjetaAutorizada { get; set; } = null!;

    public AutorizacionManipulacionHeladera()
    {
    }

    public AutorizacionManipulacionHeladera(DateTime fechaExpiracion, Heladera heladera,
        TarjetaColaboracion tarjetaAutorizada)
    {
        FechaCreacion = DateTime.UtcNow;
        FechaExpiracion = fechaExpiracion;
        Heladera = heladera;
        TarjetaAutorizada = tarjetaAutorizada;
    }
}