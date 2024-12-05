using AccesoAlimentario.Operations.Dto.Responses.Roles;

namespace AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

public class TarjetaConsumoResponse : TarjetaResponse
{
    public ColaboradorResponse Responsable { get; set; } = null!;
    public PersonaVulnerableResponse Propietario { get; set; } = null!;
}