using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AccesoHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Tarjeta Tarjeta { get; set; } = null!;
    public DateTime FechaAcceso { get; set; } = DateTime.Now;
    public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    public Heladera Heladera { get; set; } = null!;
    public AutorizacionManipulacionHeladera? Autorizacion { get; set; } = null!;
    
    public AccesoHeladera() { }
    
    public AccesoHeladera(Tarjeta tarjeta, Heladera heladera, TipoAcceso tipoAcceso, AutorizacionManipulacionHeladera autorizacion)
    {
        Tarjeta = tarjeta;
        Heladera = heladera;
        TipoAcceso = tipoAcceso;
        Autorizacion = autorizacion;
    }
}