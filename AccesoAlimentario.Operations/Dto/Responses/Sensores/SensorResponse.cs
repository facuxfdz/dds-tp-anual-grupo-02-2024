namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public abstract class SensorResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Tipo { get; set; } = string.Empty;
}