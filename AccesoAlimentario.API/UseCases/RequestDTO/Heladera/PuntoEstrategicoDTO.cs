
namespace AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

public class PuntoEstrategicoDTO
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public float? Longitud { get; set; }
    public float? Latitud { get; set; }
    public DireccionDTO? Direccion { get; set; }
}