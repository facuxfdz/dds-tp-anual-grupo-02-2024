namespace AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

public class HeladeraDTO
{
    public int? Id { get; set; }
    public PuntoEstrategicoDTO? PuntoEstrategico { get; set; } = null!;
    public ModeloHeladeraDTO? Modelo { get; set; } = null!;
    public float? TemperaturaMinima { get; set; }
    public float? TemperaturaMaxima { get; set; }
}