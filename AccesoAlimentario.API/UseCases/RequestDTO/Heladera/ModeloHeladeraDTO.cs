namespace AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

public class ModeloHeladeraDTO
{
    public string? Id { get; set; }
    public float? Capacidad { get; set; }
    public float? TemperaturaMinima { get; set; }
    public float? TemperaturaMaxima { get; set; }
}