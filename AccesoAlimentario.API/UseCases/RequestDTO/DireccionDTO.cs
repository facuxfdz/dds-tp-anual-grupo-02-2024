namespace AccesoAlimentario.API.UseCases.RequestDTO;

public class DireccionDTO
{
    public string Calle { get; set; }
    public string Numero { get; set; }
    public string Localidad { get; set; }
    public string CodigoPostal { get; set; }
    public string? Piso { get; set; }
    public string? Departamento { get; set; }
}