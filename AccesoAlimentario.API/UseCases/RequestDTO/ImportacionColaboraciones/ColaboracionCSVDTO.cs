namespace AccesoAlimentario.API.UseCases.RequestDTO.ImportacionColaboraciones;

public class ColaboracionCSVDTO
{
    public string TipoDocumento { get; set; }
    public string Documento { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Mail { get; set; }
    public string FechaColaboracion { get; set; }
    public string FormaColaboracion { get; set; }
    public int Cantidad { get; set; }
}