using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AccesoAlimentario.Core.Entities.Reportes;
public class Reporte{
    [Key]
    public string Id { get; set; }
    public string Descripcion { get; set; }
    public DateTime ValidoHasta { get; set;}
    public DateTime FechaGeneracion { get; set; }
    
    public Reporte(){}

    public Reporte(string id, string descripcion, DateTime validoHasta, DateTime fechaGeneracion){
        Id = id;
        Descripcion = descripcion;
        ValidoHasta = validoHasta;
        FechaGeneracion = fechaGeneracion; 
    }
    
    
}