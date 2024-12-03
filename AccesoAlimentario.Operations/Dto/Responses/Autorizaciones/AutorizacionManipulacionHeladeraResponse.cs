using AccesoAlimentario.Operations.Dto.Responses.Heladeras;

namespace AccesoAlimentario.Operations.Dto.Responses.Autorizaciones;

public class AutorizacionManipulacionHeladeraResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.MinValue;
    public DateTime FechaExpiracion { get; set; } = DateTime.Now;
    public HeladeraResponse Heladera { get; set; } = null!;
}