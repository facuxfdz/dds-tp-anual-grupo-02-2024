namespace AccesoAlimentario.Operations.Dto.Responses.Sensores;

public class RegistroMovimientoResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public bool Movimiento { get; set; } = false;
}