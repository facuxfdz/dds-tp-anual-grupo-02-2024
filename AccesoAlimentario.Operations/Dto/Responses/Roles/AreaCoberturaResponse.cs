namespace AccesoAlimentario.Operations.Dto.Responses.Roles;

public class AreaCoberturaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public float Latitud { get; set; } = 0;
    public float Longitud { get; set; } = 0;
    public float Radio { get; set; } = 0;
}