using AccesoAlimentario.Operations.Dto.Responses.Direcciones;

namespace AccesoAlimentario.Operations.Dto.Responses.Heladeras;

public class PuntoEstrategicoResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Nombre { get; set; } = string.Empty;
    public float Longitud { get; set; } = 0;
    public float Latitud { get; set; } = 0;
    public DireccionResponse Direccion { get; set; } = null!;
}