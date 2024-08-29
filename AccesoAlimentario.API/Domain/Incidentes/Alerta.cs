using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public abstract class Alerta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public NivelAlerta Nivel { get; set; }
    
    public Heladera Heladera { get; set; }
    
    public Alerta()
    {
    }
    
    public Alerta(DateTime fecha, NivelAlerta nivel)
    {
        Fecha = fecha;
        Nivel = nivel;
    }
}