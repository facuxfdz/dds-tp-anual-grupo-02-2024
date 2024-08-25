using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Colaboraciones;

namespace AccesoAlimentario.API.Domain.Heladeras;

public class AutorizacionHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime FechaExpiracion { get; private set; } = DateTime.Now;
    public Heladera Heladera { get; private set; } = null!;
    public TarjetaColaboracion TarjetaAutorizada { get; set; } = null!;
    
    public AutorizacionHeladera()
    {
    }
    
    public AutorizacionHeladera(DateTime fechaExpiracion, Heladera heladera,
        TarjetaColaboracion tarjetaAutorizada)
    {
        FechaCreacion = DateTime.Now;
        FechaExpiracion = fechaExpiracion;
        Heladera = heladera;
        TarjetaAutorizada = tarjetaAutorizada;
    }
}