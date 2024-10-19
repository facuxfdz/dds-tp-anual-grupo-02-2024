namespace AccesoAlimentario.Operations.Dto.Requests.Heladeras;

public class ModeloHeladeraRequest
{
    public int Capacidad { get; set; } = 0;
    public float TemperaturaMinima { get; set; } = 0;
    public float TemperaturaMaxima { get; set; } = 0;
}