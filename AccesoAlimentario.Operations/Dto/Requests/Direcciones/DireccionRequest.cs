namespace AccesoAlimentario.Operations.Dto.Requests.Direcciones;

public class DireccionRequest : IDtoRequest
{
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string Piso { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    
    public bool Validar()
    {
        return !string.IsNullOrEmpty(Calle) 
               && !string.IsNullOrEmpty(Numero) 
               && !string.IsNullOrEmpty(Localidad)
               && !string.IsNullOrEmpty(CodigoPostal);
    }
}