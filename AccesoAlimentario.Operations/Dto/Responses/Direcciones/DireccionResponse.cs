namespace AccesoAlimentario.Operations.Dto.Responses.Direcciones;

public class DireccionResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string? Piso { get; set; } = null;
    public string? Departamento { get; set; } = null;
    public string CodigoPostal { get; set; } = string.Empty;
}