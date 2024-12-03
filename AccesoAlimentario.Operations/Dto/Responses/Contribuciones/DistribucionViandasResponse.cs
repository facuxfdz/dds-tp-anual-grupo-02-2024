using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class DistribucionViandasResponse : FormaContribucionResponse
{
    public HeladeraResponse? HeladeraOrigen { get; set; } = null!;
    public HeladeraResponse? HeladeraDestino { get; set; } = null!;
    public int CantViandas { get; set; } = 0;
    public MotivoDistribucion MotivoDistribucion { get; set; } = MotivoDistribucion.Desperfecto;
}