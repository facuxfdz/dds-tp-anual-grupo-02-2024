using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AutorizacionManipulacionHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime FechaExpiracion { get; set; } = DateTime.Now;
    public Heladera Heladera { get; set; } = null!;
    public TarjetaColaboracion TarjetaAutorizada { get; set; } = null!;

    public AutorizacionManipulacionHeladera()
    {
    }

    public AutorizacionManipulacionHeladera(DateTime fechaExpiracion, Heladera heladera,
        TarjetaColaboracion tarjetaAutorizada)
    {
        FechaCreacion = DateTime.Now;
        FechaExpiracion = fechaExpiracion;
        Heladera = heladera;
        TarjetaAutorizada = tarjetaAutorizada;
    }
}