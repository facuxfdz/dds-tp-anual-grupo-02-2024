using AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

namespace AccesoAlimentario.Operations.Dto.Responses.Contribuciones;

public class RegistroPersonaVulnerableResponse : FormaContribucionResponse
{
    public TarjetaConsumoResponse Tarjeta { get; set; } = null!;
}