namespace AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;

public class DatosColaboracion
{
    public string TipoDoc { get; set; } = null!;
    public string Documento { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string FechaColaboracion { get; set; } = null!;
    public string FormaColaboracion { get; set; } = null!;
    public int Cantidad { get; set; }
}