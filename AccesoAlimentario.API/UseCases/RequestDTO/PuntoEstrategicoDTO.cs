using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class PuntoEstrategicoDTO
{
    public string Nombre { get; set; } = null!;
    public float Longitud { get; set; } = 0;
    public float Latitud { get; set; } = 0;
    public DireccionDTO Direccion { get; set; } = null!;
}