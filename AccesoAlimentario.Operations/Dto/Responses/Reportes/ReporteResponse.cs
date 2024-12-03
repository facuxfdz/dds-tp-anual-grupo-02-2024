using AccesoAlimentario.Core.Entities.Reportes;

namespace AccesoAlimentario.Operations.Dto.Responses.Reportes;

public class ReporteResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public TipoReporte Tipo { get; set; } = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime FechaExpiracion { get; set; } = DateTime.Now.AddDays(7);
    public string Cuerpo { get; set; } = string.Empty;
}