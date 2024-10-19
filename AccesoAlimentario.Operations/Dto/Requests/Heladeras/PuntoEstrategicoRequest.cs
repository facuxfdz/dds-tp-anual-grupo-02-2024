using AccesoAlimentario.Operations.Dto.Requests.Direcciones;

namespace AccesoAlimentario.Operations.Dto.Requests.Heladeras;

public class PuntoEstrategicoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public float Longitud { get; set; } = 0;
    public float Latitud { get; set; } = 0;
    public DireccionRequest Direccion { get; set; } = null!;
}