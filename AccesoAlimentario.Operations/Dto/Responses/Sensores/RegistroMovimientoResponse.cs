namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class RegistroMovimientoResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public bool Movimiento { get; set; } = false;
}