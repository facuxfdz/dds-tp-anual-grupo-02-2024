using AccesoAlimentario.Core.Entities.Reportes;

namespace AccesoAlimentario.Operations.Dto.Responses.Reportes;

public class ReporteResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public TipoReporte Tipo { get; set; } = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaExpiracion { get; set; } = DateTime.UtcNow.AddDays(7);
    public object Cuerpo { get; set; } = new { };
}