using AccesoAlimentario.Operations.Dto.Responses.Tarjetas;

namespace AccesoAlimentario.Operations.Dto.Responses.Roles;

public class PersonaVulnerableResponse : RolResponse
{
    public int CantidadDeMenores { get; set; } = 0;
    public TarjetaConsumoResponse Tarjeta { get; set; } = null!;
    public DateTime FechaRegistroSistema { get; set; } = DateTime.Now;
}