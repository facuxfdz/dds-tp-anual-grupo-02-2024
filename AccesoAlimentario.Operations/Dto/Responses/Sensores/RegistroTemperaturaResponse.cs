namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class RegistroTemperaturaResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public float Temperatura { get; set; } = 0;
}