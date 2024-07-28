using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Reportes;

public class FallasHeladeraReporteSchema
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public Heladera Heladera { get; set; }
    public bool Falla { get; set; } // Flag para indicar si hubo falla
    public DateTime? FechaFalla { get; set; }
    public bool? Resuelto { get; set; }
    public Reporte Reporte { get; set; }

    public FallasHeladeraReporteSchema()
    {
    }
    public FallasHeladeraReporteSchema(Heladera heladera, bool falla, DateTime? fechaFalla, bool? resuelto, Reporte reporte)
    {
        Heladera = heladera;
        Falla = falla;
        FechaFalla = fechaFalla;
        Resuelto = resuelto;
        Reporte = reporte;
    }
}