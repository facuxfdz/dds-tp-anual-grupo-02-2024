using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Colaboraciones;

namespace AccesoAlimentario.API.Domain.Heladeras;

public class AccesoHeladera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Tarjeta Tarjeta { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public TipoAcceso TipoAcceso { get; set; }
    public Heladera Heladera { get; set; } = null!;
    public AutorizacionHeladera? Autorizacion { get; set; } = null!;
    
    public AccesoHeladera(){}
    
    public AccesoHeladera(Tarjeta tarjeta, Heladera heladera, TipoAcceso tipoAcceso, AutorizacionHeladera? autorizacion)
    {
        Tarjeta = tarjeta;
        Heladera = heladera;
        TipoAcceso = tipoAcceso;
        Autorizacion = autorizacion;
    }
}