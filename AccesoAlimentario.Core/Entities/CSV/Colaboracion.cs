using System.Runtime.InteropServices.JavaScript;
using AccesoAlimentario.Core.Entities.Beneficiarios;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using CsvHelper.Configuration.Attributes;

namespace AccesoAlimentario.Core.Entities.CSV;

public class Colaboracion
{
    public string TipoDoc { get; set; }
    public string Documento { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Mail { get; set; }
    public string FechaColaboracion { get; set; }
    public string FormaColaboracion { get; set; }
    public int Cantidad { get; set; }

    
}