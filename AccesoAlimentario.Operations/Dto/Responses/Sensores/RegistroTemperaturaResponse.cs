namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class RegistroTemperaturaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public float Temperatura { get; set; } = 0;
}