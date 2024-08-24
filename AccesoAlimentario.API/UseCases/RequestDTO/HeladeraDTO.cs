using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class HeladeraDTO
{
    public Direccion DireccionPuntoEstrategico { get; set; }
    public float TemperaturaMinimaConfig { get; set; }
    public float TemperaturaMaximaConfig { get; set; }
    public string ModeloId { get; set; }
}