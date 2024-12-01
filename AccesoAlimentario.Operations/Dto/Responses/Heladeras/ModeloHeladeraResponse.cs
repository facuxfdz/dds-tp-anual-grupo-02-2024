namespace AccesoAlimentario.Operations.Dto.Responses.Heladeras;

public class ModeloHeladeraResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public int Capacidad { get; set; } = 0;
    public int TemperaturaMinima { get; set; } = 0;
}